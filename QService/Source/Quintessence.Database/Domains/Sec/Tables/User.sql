CREATE TABLE [dbo].[User]
(
	[Id]							UNIQUEIDENTIFIER	NOT NULL,
	[AssociateId]					INT					NULL,
	[RoleId]						UNIQUEIDENTIFIER	NULL,
	[UserName]						NVARCHAR(MAX)		NOT NULL,
	[FirstName]						NVARCHAR(MAX)		NOT NULL,
	[LastName]						NVARCHAR(MAX)		NOT NULL,
	[Email]							NVARCHAR(MAX)		NULL,
	[Comment]						NVARCHAR(MAX)		NULL,
	[Password]						NVARCHAR(MAX)		NOT NULL,
	[ChangePassword]				BIT					NOT NULL	DEFAULT 0,
	[IsEmployee]					BIT					NOT NULL	DEFAULT 0,
	[IsLocked]						BIT					NOT NULL	DEFAULT 0,
	[Audit_CreatedBy]				NVARCHAR(MAX)		NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]				DATETIME			NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]				NVARCHAR(MAX)		NULL,
	[Audit_ModifiedOn]				DATETIME			NULL,
	[Audit_DeletedBy]				NVARCHAR(MAX)		NULL,
	[Audit_DeletedOn]				DATETIME			NULL,
	[Audit_IsDeleted]				BIT					NOT NULL	DEFAULT 0,
	[Audit_VersionId]				UNIQUEIDENTIFIER	NOT NULL	DEFAULT NEWID()
)