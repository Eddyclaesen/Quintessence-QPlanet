CREATE VIEW [dbo].[ReportStatusView] AS
	SELECT		*
	FROM		[dbo].[ReportStatus]	WITH (READCOMMITTED)
