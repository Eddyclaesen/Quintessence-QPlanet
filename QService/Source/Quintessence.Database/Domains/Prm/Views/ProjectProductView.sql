CREATE VIEW [dbo].[ProjectProductView] AS
	SELECT		[ProjectProduct].*,
				[ProductTypeView].[Name]			AS	[ProductTypeName]
				
	FROM		[ProjectProduct]	WITH (NOLOCK)

	INNER JOIN	[ProductTypeView]
		ON		[ProductTypeView].[Id] = [ProjectProduct].[ProductTypeId]
	
	WHERE		[ProjectProduct].[Audit_IsDeleted] = 0