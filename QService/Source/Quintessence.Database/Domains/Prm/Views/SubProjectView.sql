CREATE VIEW [dbo].[SubProjectView] AS
	SELECT		[ProjectView].[Id]			AS	[Id],
				[SubProject].[ProjectId]	AS	[MainProjectId],
				[ProjectView].[Name]		AS	[Name],
				[ProjectTypeView].[Name]	AS	[ProjectTypeName]

	FROM		[SubProject]	WITH (NOLOCK)

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [SubProject].[SubProjectId]

	INNER JOIN	[ProjectTypeView]
		ON		[ProjectTypeView].[Id] = [ProjectView].[ProjectTypeId]
