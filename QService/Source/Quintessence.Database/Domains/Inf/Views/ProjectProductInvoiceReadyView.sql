CREATE VIEW [dbo].[ProjectProductInvoiceReadyView]
	AS 
	SELECT		[ProjectProductView].[Id]						AS	[Id],
				[ProjectProductView].[ProductTypeName]			AS	[ProductTypeName],
				[ProjectProductView].[Description]				AS	[Description],
				[ProjectProductView].[InvoiceStatusCode]		AS	[InvoiceStatusCode],
				[ProjectProductView].[InvoiceAmount]			AS	[InvoiceAmount],
				[ProjectProductView].[Audit_CreatedBy]			AS	[Audit_CreatedBy],
				[ProjectProductView].[Audit_CreatedOn]			AS	[Audit_CreatedOn],
				[ProjectProductView].[Audit_DeletedBy]			AS	[Audit_DeletedBy],
				[ProjectProductView].[Audit_DeletedOn]			AS	[Audit_DeletedOn],
				[ProjectProductView].[Audit_IsDeleted]			AS	[Audit_IsDeleted],
				[ProjectProductView].[Audit_ModifiedBy]			AS	[Audit_ModifiedBy],
				[ProjectProductView].[Audit_ModifiedOn]			AS	[Audit_ModifiedOn],
				[ProjectProductView].[Audit_VersionId]			AS	[Audit_VersionId]


	FROM		[ProjectProductView]

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ProjectProductView].[ProjectId]

	WHERE		[ProjectProductView].[InvoiceStatusCode] = 20