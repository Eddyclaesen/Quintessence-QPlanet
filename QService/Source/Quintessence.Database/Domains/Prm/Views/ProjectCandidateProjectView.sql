CREATE VIEW [dbo].[ProjectCandidateProjectView] AS
	SELECT		[ProjectCandidateView].[Id]						AS	ProjectCandidateId,
				[SubProject].[SubProjectId]						AS	SubProjectId,
				[ProjectView].[Name]							AS	ProjectName,
				[ProjectTypeView].[Name]						AS	ProjectTypeName

	FROM		[SubProject]

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [SubProject].[SubProjectId]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [SubProject].[ProjectCandidateId]

	INNER JOIN	[ProjectTypeView]
		ON		[ProjectTypeView].[Id] = [ProjectView].[ProjectTypeId]
	