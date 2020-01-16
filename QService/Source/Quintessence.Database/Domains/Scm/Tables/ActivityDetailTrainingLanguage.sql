﻿CREATE TABLE [dbo].[ActivityDetailTrainingLanguage](
	[Id]						UNIQUEIDENTIFIER		NOT NULL,
	[ActivityDetailTrainingId]	UNIQUEIDENTIFIER		NOT NULL,
	[LanguageId]				INT						NOT NULL,
	[SessionQuantity]			INT						NOT NULL,
	[Audit_CreatedBy]			NVARCHAR(MAX)			NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]			DATETIME				NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]			NVARCHAR(MAX)			NULL,
	[Audit_ModifiedOn]			DATETIME				NULL,
	[Audit_DeletedBy]			NVARCHAR(MAX)			NULL,
	[Audit_DeletedOn]			DATETIME				NULL,
	[Audit_IsDeleted]			BIT						NOT NULL	DEFAULT 0,
	[Audit_VersionId]			UNIQUEIDENTIFIER		NOT NULL	DEFAULT NEWID()
)