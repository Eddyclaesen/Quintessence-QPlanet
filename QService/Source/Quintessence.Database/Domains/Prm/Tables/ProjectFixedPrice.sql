﻿CREATE TABLE [dbo].[ProjectFixedPrice](
	[Id]						UNIQUEIDENTIFIER		NOT NULL,
	[ProjectId]					UNIQUEIDENTIFIER		NOT NULL,
	[Amount]					DECIMAL(18,2)			NOT NULL,
	[InvoiceStatusCode]			INT						NOT NULL,
	[InvoicedDate]				DATETIME				NULL,
	[InvoiceRemarks]			NVARCHAR(MAX)			NULL,
	[PurchaseOrderNumber]		NVARCHAR(MAX)			NULL,
	[InvoiceNumber]				NVARCHAR(MAX)			NULL,
	[Deadline]					DATETIME				NOT NULL,
	[ProposalId]				UNIQUEIDENTIFIER		NULL,
	[Audit_CreatedBy]			NVARCHAR(MAX)			NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]			DATETIME				NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]			NVARCHAR(MAX)			NULL,
	[Audit_ModifiedOn]			DATETIME				NULL,
	[Audit_DeletedBy]			NVARCHAR(MAX)			NULL,
	[Audit_DeletedOn]			DATETIME				NULL,
	[Audit_IsDeleted]			BIT						NOT NULL	DEFAULT 0,
	[Audit_VersionId]			UNIQUEIDENTIFIER		NOT NULL	DEFAULT NEWID()
)