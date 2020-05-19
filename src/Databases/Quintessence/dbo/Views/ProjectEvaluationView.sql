CREATE VIEW [dbo].[ProjectEvaluationView] AS
	SELECT		[ProjectEvaluation].*

	FROM		[ProjectEvaluation]	WITH (NOLOCK)

	WHERE		[ProjectEvaluation].[Audit_IsDeleted] = 0