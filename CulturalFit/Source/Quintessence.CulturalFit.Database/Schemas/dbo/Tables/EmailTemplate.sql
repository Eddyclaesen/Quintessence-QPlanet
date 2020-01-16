CREATE TABLE [dbo].[EmailTemplate]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [Subject] NVARCHAR(50) NOT NULL, 
    [Body] NVARCHAR(MAX) NOT NULL, 
    [LanguageId] INT NOT NULL, 
    [TheoremListRequestTypeId] INT NOT NULL, 
    [Audit_CreatedBy] NVARCHAR(50) NOT NULL, 
    [Audit_CreatedOn] DATETIME NOT NULL, 
    [Audit_ModifiedBy] NVARCHAR(50) NULL, 
    [Audit_ModifiedOn] DATETIME NULL, 
    [Audit_DeletedBy] NVARCHAR(50) NULL, 
    [Audit_DeletedOn] DATETIME NULL, 
    [Audit_IsDeleted] BIT NOT NULL, 
    [Audit_VersionId] UNIQUEIDENTIFIER NOT NULL
)
