CREATE TABLE [dbo].[ContactPerson]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [Gender] INT NOT NULL, 
	[LanguageId] INT NOT NULL,
    [Audit_CreatedBy] NVARCHAR(50) NOT NULL, 
    [Audit_CreatedOn] DATETIME NOT NULL, 
    [Audit_ModifiedBy] NVARCHAR(50) NULL, 
    [Audit_ModifiedOn] DATETIME NULL, 
    [Audit_DeletedBy] NVARCHAR(50) NULL, 
    [Audit_DeletedOn] DATETIME NULL, 
    [Audit_IsDeleted] BIT NOT NULL, 
    [Audit_VersionId] UNIQUEIDENTIFIER NOT NULL
)
