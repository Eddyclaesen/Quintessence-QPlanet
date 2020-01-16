CREATE TABLE [dbo].[Candidate]
(
	[Id]					UNIQUEIDENTIFIER	NOT NULL, 
    [FirstName]				NVARCHAR(MAX)		NOT NULL, 
    [LastName]				NVARCHAR(MAX)		NOT NULL, 
    [Email]					NVARCHAR(MAX)		NULL,
    [Gender]				CHAR(1)				NOT NULL,
    [LanguageId]			INTEGER				NOT NULL,
	[Audit_CreatedBy]		NVARCHAR(MAX)		NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]		DATETIME			NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]		NVARCHAR(MAX)		NULL,
	[Audit_ModifiedOn]		DATETIME			NULL,
	[Audit_DeletedBy]		NVARCHAR(MAX)		NULL,
	[Audit_DeletedOn]		DATETIME			NULL,
	[Audit_IsDeleted]		BIT					NOT NULL	DEFAULT 0,
	[Audit_VersionId]		UNIQUEIDENTIFIER	NOT NULL	DEFAULT NEWID(),
	[LegacyId]				INT					NULL
)
