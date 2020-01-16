CREATE TABLE [dbo].[TheoremListRequest]
(
	[Id]							UNIQUEIDENTIFIER	NOT NULL,
	[ContactId]						INT					NOT NULL,
	[ContactPersonId]				UNIQUEIDENTIFIER	NULL,
	[TheoremListCandidateId]		UNIQUEIDENTIFIER	NULL,
	[RequestDate]					DATETIME			NOT NULL,
	[Deadline]						DATETIME			NOT NULL,
	[TheoremListRequestTypeId]		INT					NOT NULL, 
	[VerificationCode]				VARCHAR(6)			NOT NULL, 
	[IsMailSent]						BIT					NOT NULL,
    [Audit_CreatedBy]				NVARCHAR(50)		NOT NULL, 
    [Audit_CreatedOn]				DATETIME			NOT NULL, 
    [Audit_ModifiedBy]				NVARCHAR(50)		NULL, 
    [Audit_ModifiedOn]				DATETIME			NULL, 
    [Audit_DeletedBy]				NVARCHAR(50)		NULL, 
    [Audit_DeletedOn]				DATETIME			NULL, 
    [Audit_IsDeleted]				BIT					NOT NULL, 
    [Audit_VersionId]				UNIQUEIDENTIFIER	NOT NULL
)
