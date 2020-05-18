CREATE VIEW [dbo].[MailTemplateView] AS
	SELECT		*
	FROM		[dbo].[MailTemplate]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0