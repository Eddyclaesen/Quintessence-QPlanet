CREATE VIEW [dbo].[ProductTypeView] AS
	SELECT		[ProductType].*
	FROM		[ProductType]	WITH (NOLOCK)
	WHERE		[ProductType].[Audit_IsDeleted] = 0