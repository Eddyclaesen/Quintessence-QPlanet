CREATE TABLE [dbo].[ProjectTypeCategory](
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[Name]					NVARCHAR(100)		NOT NULL,
	[Code]					VARCHAR(10)			NOT NULL,
	[SubCategoryType]		INT					NULL,
	[Execution]				INT					NULL,
	[Color]					VARCHAR(6)			NULL,
	[CrmTaskId]				INT					NULL,
	[Audit_CreatedBy]		NVARCHAR(MAX)		NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]		DATETIME			NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]		NVARCHAR(MAX)		NULL,
	[Audit_ModifiedOn]		DATETIME			NULL,
	[Audit_DeletedBy]		NVARCHAR(MAX)		NULL,
	[Audit_DeletedOn]		DATETIME			NULL,
	[Audit_IsDeleted]		BIT					NOT NULL	DEFAULT 0,
	[Audit_VersionId]		UNIQUEIDENTIFIER	NOT NULL	DEFAULT NEWID()
)