CREATE VIEW [dbo].[MailTemplateTranslationView] AS
	SELECT		*
	FROM		[dbo].[MailTemplateTranslation]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0