CREATE VIEW [dbo].[ProjectCandidateCompetenceScoreView] AS
	SELECT		[ProjectCandidateCompetenceScore].*
				
	FROM		[ProjectCandidateCompetenceScore]	WITH (NOLOCK)

	WHERE		[ProjectCandidateCompetenceScore].[Audit_IsDeleted] = 0