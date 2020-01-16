CREATE VIEW [dbo].[ProjectTypeCategoryLevelView] AS
	SELECT		[ProjectTypeCategoryLevel].*
				
	FROM		[ProjectTypeCategoryLevel]	WITH (READCOMMITTED)

	WHERE		[ProjectTypeCategoryLevel].[Audit_IsDeleted] = 0