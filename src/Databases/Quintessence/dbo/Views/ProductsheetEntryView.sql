
CREATE VIEW [dbo].[ProductsheetEntryView] AS
	SELECT		[ProductsheetEntry].*
	FROM		[ProductsheetEntry]	WITH (NOLOCK)

	INNER JOIN	[ProductView]
		ON		[ProductView].[Id] = [ProductsheetEntry].[ProductId]

	INNER JOIN	[ProductTypeView]
		ON		[ProductTypeView].[Id] = [ProductView].[ProductTypeId]

	WHERE		[ProductsheetEntry].[Audit_IsDeleted] = 0
