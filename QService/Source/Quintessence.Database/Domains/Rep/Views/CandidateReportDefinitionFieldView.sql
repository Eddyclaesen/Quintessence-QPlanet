CREATE VIEW [dbo].[CandidateReportDefinitionFieldView] AS
	SELECT		*

	FROM		[CandidateReportDefinitionField]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0
