CREATE PROCEDURE [dbo].[Project_SearchCrmProjects]
	@ProjectId				UNIQUEIDENTIFIER = NULL,
	@ProjectName			NVARCHAR(MAX) = NULL,
	@WithPlannedStatus		BIT = 0,
	@WithRunningStatus		BIT = 0,
	@WithDoneStatus			BIT = 0,
	@WithStoppedStatus		BIT = 0
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusTable AS TABLE(StatusId int, [Include] BIT)
	INSERT INTO @StatusTable(StatusId, [Include]) VALUES (2, @WithPlannedStatus)
	INSERT INTO @StatusTable(StatusId, [Include]) VALUES (3, @WithRunningStatus)
	INSERT INTO @StatusTable(StatusId, [Include]) VALUES (4, @WithDoneStatus)
	INSERT INTO @StatusTable(StatusId, [Include]) VALUES (5, @WithStoppedStatus)		

	IF @ProjectId IS NOT NULL
		BEGIN
			SELECT		DISTINCT
						[CrmProjectView].[Id] AS CrmProjectId,
						[CrmProjectView].[Name] AS CrmProjectName,
						[CrmProjectStatusView].[Name] AS CrmProjectStatusName

			FROM		[CrmProjectView]

			INNER JOIN	[ProjectView] 
				ON		[ProjectView].[ContactId] = [CrmProjectView].[ContactId]

			INNER JOIN	[CrmProjectStatusView] 
				ON		[CrmProjectStatusView].[Id] = [CrmProjectView].[ProjectStatusId]

			LEFT JOIN	[Project2CrmProjectView] 
				ON		[Project2CrmProjectView].[CrmProjectId] = [CrmProjectView].[Id]

			LEFT JOIN	[ProjectView] LinkedProjectView 
				ON		LinkedProjectView.[Id] = [Project2CrmProjectView].[ProjectId]

			WHERE		[ProjectView].[Id] = @ProjectId
				AND		[CrmProjectView].[ProjectStatusId] IN (SELECT StatusId FROM @StatusTable WHERE [Include] = 1)
				AND		(@ProjectName IS NULL OR [CrmProjectView].[Name] LIKE '%' + @ProjectName + '%')
		END

	END
GO
