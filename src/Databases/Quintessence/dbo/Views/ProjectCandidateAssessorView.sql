CREATE VIEW [dbo].[ProjectCandidateAssessorView] AS
	SELECT		[ProjectCandidateAssessor].*

	FROM		[ProjectCandidateAssessor]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = [ProjectCandidateAssessor].[ProjectCandidateId]

	INNER JOIN	[UserView]
		ON		[UserView].[Id] = [ProjectCandidateAssessor].[UserId]