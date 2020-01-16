CREATE PROCEDURE [dbo].[CrmReplicationPerson_Sync]
AS
BEGIN
	DECLARE	@Person	AS	TABLE(								
								[Id]							INT			NOT NULL,
								[ContactId]						INT,
								[FirstName]						NVARCHAR(MAX),
								[LastName]						NVARCHAR(MAX),
								[Title]							NVARCHAR(MAX),
								[IsRetired]						BIT)

	INSERT INTO @Person(	[Id],
							[ContactId],
							[FirstName],
							[LastName],
							[Title],
							[IsRetired])
		SELECT		[Person].[Person_id]		AS	[Id],
					[Person].[Contact_id]		AS	[ContactId],
					[Person].[Firstname]		AS	[FirstName],
					[Person].[Lastname]			AS	[LastName],
					[Person].[Title]			AS	[Title],
					[Person].[retired]			AS	[IsRetired]
		FROM        [$(superoffice7server)].[$(SuperOffice7)].[dbo].[Person]		
		
	MERGE [CrmReplicationPerson] AS [TargetDatabase]
	USING @Person AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[ContactId] =		[SourceDatabase].[ContactId],
					[TargetDatabase].[FirstName] =		[SourceDatabase].[FirstName],
					[TargetDatabase].[LastName]	 =		[SourceDatabase].[LastName],
					[TargetDatabase].[Title] =			[SourceDatabase].[Title],
					[TargetDatabase].[IsRetired] =		[SourceDatabase].[IsRetired]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[ContactId],
				[FirstName],
				[LastName],
				[Title],
				[IsRetired])
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[ContactId],
				[SourceDatabase].[FirstName],
				[SourceDatabase].[LastName],
				[SourceDatabase].[Title],
				[SourceDatabase].[IsRetired]);
END
GO