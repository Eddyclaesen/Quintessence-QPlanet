CREATE VIEW [dbo].[ProjectCandidateResumeView] AS
	SELECT		[ProjectCandidateResume].*
				
	FROM		[ProjectCandidateResume]	WITH (NOLOCK)

	WHERE		[ProjectCandidateResume].[Audit_IsDeleted] = 0