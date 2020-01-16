CREATE VIEW [dbo].[ProjectTypeCategoryDefaultValueView] AS
	SELECT		*
	FROM		[ProjectTypeCategoryDefaultValue]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0
