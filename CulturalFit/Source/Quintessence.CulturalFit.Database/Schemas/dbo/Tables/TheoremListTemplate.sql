CREATE TABLE [dbo].[TheoremListTemplate]
(
	[Id]	UNIQUEIDENTIFIER	NOT NULL, 
    [Name]	 NVARCHAR(MAX)		NOT NULL, 
    [Audit_CreatedBy] NVARCHAR(50) NOT NULL, 
    [Audit_CreatedOn] DATETIME NOT NULL, 
    [Audit_ModifiedBy] NVARCHAR(50) NULL, 
    [Audit_ModifiedOn] DATETIME NULL, 
    [Audit_DeletedBy] NVARCHAR(50) NULL, 
    [Audit_DeletedOn] DATETIME NULL, 
    [Audit_IsDeleted] BIT NOT NULL, 
    [Audit_VersionId] UNIQUEIDENTIFIER NOT NULL
)
