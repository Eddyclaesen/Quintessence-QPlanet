CREATE VIEW [dbo].[ReportParameterView] AS
	SELECT		[ReportParameter].*

	FROM		[ReportParameter]	WITH (NOLOCK)

	WHERE		[ReportParameter].[Audit_IsDeleted] = 0