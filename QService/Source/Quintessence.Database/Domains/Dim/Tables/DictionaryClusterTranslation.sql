﻿CREATE TABLE [dbo].[DictionaryClusterTranslation]
(
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[LanguageId]			INT					NOT NULL,
	[DictionaryClusterId]	UNIQUEIDENTIFIER	NOT NULL,
	[Text]					TEXT				NOT NULL,
	[Description]			TEXT				NULL,
	[Audit_CreatedBy]		NVARCHAR(MAX)		NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]		DATETIME			NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]		NVARCHAR(MAX)		NULL,
	[Audit_ModifiedOn]		DATETIME			NULL,
	[Audit_DeletedBy]		NVARCHAR(MAX)		NULL,
	[Audit_DeletedOn]		DATETIME			NULL,
	[Audit_IsDeleted]		BIT					NOT NULL	DEFAULT 0,
	[Audit_VersionId]		UNIQUEIDENTIFIER	NOT NULL	DEFAULT NEWID()
)
