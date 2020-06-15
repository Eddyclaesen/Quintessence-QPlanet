CREATE VIEW [dbo].[ProjectCandidateClusterScoreView] AS
	SELECT		[ProjectCandidateClusterScore].*
				
	FROM		[ProjectCandidateClusterScore]	WITH (NOLOCK)

	WHERE		[ProjectCandidateClusterScore].[Audit_IsDeleted] = 0