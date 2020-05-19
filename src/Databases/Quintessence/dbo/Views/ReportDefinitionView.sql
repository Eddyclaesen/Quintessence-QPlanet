CREATE VIEW [dbo].[ReportDefinitionView] AS
	SELECT		[ReportDefinition].*

	FROM		[ReportDefinition]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0