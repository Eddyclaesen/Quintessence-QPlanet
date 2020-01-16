CREATE VIEW [dbo].[MainProjectView] AS
	SELECT		[ProjectView].[Id]			AS	[Id],
				[SubProject].[SubProjectId]	AS	[SubProjectId],
				[ProjectView].[Name]		AS	[Name],
				[ProjectTypeView].[Name]	AS	[ProjectTypeName]

	FROM		[SubProject]	WITH (NOLOCK)

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [SubProject].[ProjectId]

	INNER JOIN	[ProjectTypeView]
		ON		[ProjectTypeView].[Id] = [ProjectView].[ProjectTypeId]

	WHERE		[SubProject].ProjectCandidateId IS NULL
