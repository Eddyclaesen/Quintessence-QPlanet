CREATE VIEW [dbo].[ProjectTypeCategoryTranslationView] AS
	SELECT		[ProjectTypeCategoryTranslation].*

	FROM		[ProjectTypeCategoryTranslation]	WITH	(NOLOCK)

 	WHERE		[ProjectTypeCategoryTranslation].[Audit_IsDeleted] = 0
