CREATE TABLE [dbo].[CandidateReportDefinitionField](
	[Id]							UNIQUEIDENTIFIER		NOT NULL,
	[CandidateReportDefinitionId]	UNIQUEIDENTIFIER		NOT NULL,
	[Name]							NVARCHAR(MAX)			NOT NULL,
	[Code]							NVARCHAR(MAX)			NOT NULL,
	[IsActive]						BIT						NOT NULL	DEFAULT 1,
	[Audit_CreatedBy]				NVARCHAR(MAX)			NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]				DATETIME				NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]				NVARCHAR(MAX)			NULL,
	[Audit_ModifiedOn]				DATETIME				NULL,
	[Audit_DeletedBy]				NVARCHAR(MAX)			NULL,
	[Audit_DeletedOn]				DATETIME				NULL,
	[Audit_IsDeleted]				BIT						NOT NULL	DEFAULT 0,
	[Audit_VersionId]				UNIQUEIDENTIFIER		NOT NULL	DEFAULT NEWID()
)