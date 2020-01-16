CREATE VIEW [dbo].[ProductView] AS
	SELECT		[Product].[Id],
				[Product].[ProjectId],
				[Product].[Name],
				[Product].[ProductTypeId],
				[Product].[UnitPrice],
				[Product].[Description],
				[Product].[Audit_CreatedBy],
				[Product].[Audit_CreatedOn],
				[Product].[Audit_ModifiedBy],
				[Product].[Audit_ModifiedOn],
				[Product].[Audit_DeletedBy],
				[Product].[Audit_DeletedOn],
				[Product].[Audit_IsDeleted],
				[Product].[Audit_VersionId],
				[ProductTypeView].[Name]		AS	ProductTypeName

	FROM		[Product]	WITH (NOLOCK)

	INNER JOIN	[ProductTypeView]
		ON		[ProductTypeView].[Id] = [Product].[ProductTypeId]

	WHERE		[Product].[Audit_IsDeleted] = 0