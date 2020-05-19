CREATE PROCEDURE [dbo].[CrmReplicationProject_Sync]
AS
BEGIN
	DECLARE @BookyearTable AS TABLE(BookyearName VARCHAR(5), StartDate DATETIME, EndDate DATETIME)
	
	DECLARE @Year AS INT = 2013
	WHILE @Year - 1 <= YEAR(GETDATE())
	BEGIN
		INSERT INTO @BookyearTable
			SELECT CAST(@Year - 2000 AS VARCHAR) + '/' + CAST(@Year - 1999 AS VARCHAR), CAST(@Year AS VARCHAR) + '-04-01 00:00:00', CAST(@Year + 1 AS VARCHAR) + '-03-31 23:59:59'
			
		SET @Year = @Year + 1
	END

	--Merge Project (further merge project status)
	DECLARE	@Project	AS	TABLE(								
								[Id]					INT			NOT NULL,
								[Name]					NVARCHAR(MAX),
								[AssociateId]			INT,
								[ContactId]				INT,
								[ProjectStatusId]		INT,
								[StartDate]				DATETIME,
								[BookyearFrom]			DATETIME,
								[BookyearTo]			DATETIME)

	INSERT INTO @Project(	[Id],
							[Name],
							[AssociateId],
							[ContactId],
							[ProjectStatusId],
							[StartDate],
							[BookyearFrom],
							[BookyearTo])
		SELECT 		DISTINCT p.project_id AS Id, 
					p.name, 
					p.associate_id AS AssociateId, 
					pm.contact_id AS ContactId, 
					p.status_idx AS ProjectStatusId,
					p.registered AS StartDate,
					BookyearTable.StartDate,
					BookyearTable.EndDate

		FROM		[Superoffice7].[CRM7].PROJECT AS p 
		
		LEFT JOIN	[Superoffice7].[CRM7].PROJECTMEMBER AS pm 
			ON		pm.project_id = p.project_id

		LEFT JOIN	[Superoffice7].[CRM7].[UDPROJECTSMALL] AS ProjectSmall
			ON		ProjectSmall.[udprojectSmall_id] = p.userdef_id
		
		LEFT JOIN	[Superoffice7].[CRM7].[UDLIST] AS List
			ON		List.[UDList_id] = ProjectSmall.[Long30]
		
		LEFT JOIN	@BookyearTable BookyearTable
			ON		BookyearTable.BookyearName COLLATE Latin1_General_CS_AS = List.name COLLATE Latin1_General_CS_AS

		WHERE		pm.contact_id IS NOT NULL
			AND		pm.contact_id <> 3

		--ORDER BY	pm.registered

	INSERT INTO @Project(	[Id],
							[Name],
							[AssociateId],
							[ContactId],
							[ProjectStatusId],
							[StartDate],
							[BookyearFrom],
							[BookyearTo])
		SELECT 		DISTINCT p.project_id AS Id, 
					p.name, 
					p.associate_id AS AssociateId, 
					pm.contact_id AS ContactId, 
					p.status_idx AS ProjectStatusId,
					p.registered AS StartDate,
					BookyearTable.StartDate,
					BookyearTable.EndDate

		FROM		[Superoffice7].[CRM7].PROJECT AS p 
		
		LEFT JOIN	[Superoffice7].[CRM7].PROJECTMEMBER AS pm 
			ON		pm.project_id = p.project_id

		LEFT JOIN	[Superoffice7].[CRM7].[UDPROJECTSMALL] AS ProjectSmall
			ON		ProjectSmall.[udprojectSmall_id] = p.userdef_id
		
		LEFT JOIN	[Superoffice7].[CRM7].[UDLIST] AS List
			ON		List.[UDList_id] = ProjectSmall.[Long30]
		
		LEFT JOIN	@BookyearTable BookyearTable
			ON		BookyearTable.BookyearName COLLATE Latin1_General_CS_AS = List.name COLLATE Latin1_General_CS_AS

		WHERE		pm.contact_id IS NOT NULL
			AND		pm.contact_id = 3
			AND		p.project_id NOT IN (SELECT [Id] FROM @Project WHERE [ContactId] <> 3)
		
	MERGE [CrmReplicationProject] AS [TargetDatabase]
	USING @Project AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	 AND [TargetDatabase].[ContactId] =	[SourceDatabase].[ContactId]
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[Name] =				[SourceDatabase].[Name],
					[TargetDatabase].[AssociateId] =		[SourceDatabase].[AssociateId],
					[TargetDatabase].[ContactId] =			[SourceDatabase].[ContactId],
					[TargetDatabase].[ProjectStatusId]	=	[SourceDatabase].[ProjectStatusId],
					[TargetDatabase].[StartDate]	=		[SourceDatabase].[StartDate],
					[TargetDatabase].[BookyearFrom]	=		ISNULL([SourceDatabase].[BookyearFrom], '2012-04-01 00:00:00'),
					[TargetDatabase].[BookyearTo]	=		ISNULL([SourceDatabase].[BookyearTo], '2013-03-31 23:59:59')
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[Name],
				[AssociateId],
				[ContactId],
				[ProjectStatusId],
				[StartDate],
				[BookyearFrom],
				[BookyearTo]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[Name],
				[SourceDatabase].[AssociateId],
				[SourceDatabase].[ContactId],
				[SourceDatabase].[ProjectStatusId],
				[SourceDatabase].[StartDate],
				ISNULL([SourceDatabase].[BookyearFrom], '2012-04-01 00:00:00'),
				ISNULL([SourceDatabase].[BookyearTo], '2013-03-31 23:59:59'));

	--Merge projectstatus
	DECLARE	@ProjectStatus	AS	TABLE(								
								[Id]					INT			NOT NULL,
								[Name]					NVARCHAR(MAX))

	INSERT INTO @ProjectStatus([Id], [Name])
		SELECT		DISTINCT 
					ps.ProjStatus_id AS Id, 
					ps.name as Name

		FROM		[Superoffice7].[CRM7].PROJSTATUS ps
		WHERE		ps.ProjStatus_id IN (3,4)
		
	MERGE [CrmReplicationProjectStatus] AS [TargetDatabase]
	USING @ProjectStatus AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[Name] =				[SourceDatabase].[Name]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[Name]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[Name]);
END