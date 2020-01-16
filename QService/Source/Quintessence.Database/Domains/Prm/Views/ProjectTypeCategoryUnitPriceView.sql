CREATE VIEW [dbo].[ProjectTypeCategoryUnitPriceView] AS
	SELECT		[ProjectTypeCategoryUnitPrice].*
				
	FROM		[ProjectTypeCategoryUnitPrice]	WITH (READCOMMITTED)

	WHERE		[ProjectTypeCategoryUnitPrice].[Audit_IsDeleted] = 0