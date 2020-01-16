CREATE TABLE [dbo].[TheoremList]
(
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[TheoremListRequestId]	UNIQUEIDENTIFIER	NOT NULL,
	[Name]					NVARCHAR(MAX)		NOT NULL,
		[IsCompleted]					BIT					NOT NULL DEFAULT 0,
	[TheoremListTypeId]		INT					NOT NULL, 
	[VerificationCode]		VARCHAR(6)			NOT NULL, 
    [Audit_CreatedBy]		NVARCHAR(50)		NOT NULL, 
    [Audit_CreatedOn]		DATETIME			NOT NULL, 
    [Audit_ModifiedBy]		NVARCHAR(50)		NULL, 
    [Audit_ModifiedOn]		DATETIME			NULL, 
    [Audit_DeletedBy]		NVARCHAR(50)		NULL, 
    [Audit_DeletedOn]		DATETIME			NULL, 
    [Audit_IsDeleted]		BIT					NOT NULL, 
    [Audit_VersionId]		UNIQUEIDENTIFIER	NOT NULL
)
