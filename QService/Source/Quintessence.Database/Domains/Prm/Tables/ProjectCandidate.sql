﻿CREATE TABLE [dbo].[ProjectCandidate](
	[Id]								UNIQUEIDENTIFIER	NOT NULL, 
    [CandidateId]						UNIQUEIDENTIFIER	NOT NULL, 
    [CrmCandidateAppointmentId]			INT					NOT NULL,  
    [CrmCandidateInfoId]				INT					NOT NULL, 
    [ProjectId]							UNIQUEIDENTIFIER	NOT NULL,
    [ReportDeadline]					DATETIME			NOT NULL,
	[ReportLanguageId]					INT					NOT NULL,
	[ReportReviewerId]					UNIQUEIDENTIFIER	NULL,
	[ReportStatusId]					INT					NOT NULL,
	[IsCancelled]						BIT					NOT NULL	DEFAULT 0,
	[CancelledDate]						DATETIME			NULL,
	[CancelledAppointmentDate]			DATETIME			NULL,
	[CancelledReason]					NVARCHAR(MAX)		NULL,
	[InvoiceAmount]						DECIMAL(18,2)		NULL,
	[InvoiceStatusCode]					INT					NOT NULL,
	[InvoicedDate]						DATETIME			NULL,
	[InvoiceRemarks]					NVARCHAR(MAX)		NULL,
	[PurchaseOrderNumber]				NVARCHAR(MAX)		NULL,
	[InvoiceNumber]						NVARCHAR(MAX)		NULL,
	[Remarks]							NVARCHAR(MAX)		NULL,
	[ScoringCoAssessorId]				UNIQUEIDENTIFIER	NULL,
	[IsAccompaniedByCustomer]			BIT					NOT NULL	DEFAULT 0,
	[FollowUpDone]						BIT					NOT NULL	DEFAULT 0,
	[OrderConfirmationSentDate]			DATETIME			NULL,
	[OrderConfirmationReceivedDate]		DATETIME			NULL,
	[InvitationSentDate]				DATETIME			NULL,
	[LeafletSentDate]					DATETIME			NULL,
	[ReportMailSentDate]				DATETIME			NULL,
	[DossierReadyDate]					DATETIME			NULL,
	[ReportDeadlineDone]				BIT					NOT NULL	DEFAULT 0,
	[OrderConfirmationSentDateDone]		BIT					NOT NULL	DEFAULT 0,
	[OrderConfirmationReceivedDateDone]	BIT					NOT NULL	DEFAULT 0,
	[InvitationSentDateDone]			BIT					NOT NULL	DEFAULT 0,
	[LeafletSentDateDone]				BIT					NOT NULL	DEFAULT 0,
	[ReportMailSentDateDone]			BIT					NOT NULL	DEFAULT 0,
	[DossierReadyDateDone]				BIT					NOT NULL	DEFAULT 0,
	[Extra1]							NVARCHAR(MAX)		NULL,
	[Extra2]							NVARCHAR(MAX)		NULL,
	[Extra1Done]						BIT					NOT NULL	DEFAULT 0,
	[Extra2Done]						BIT					NOT NULL	DEFAULT 0,
	[ProposalId]						UNIQUEIDENTIFIER	NULL,
	[Audit_CreatedBy]					NVARCHAR(MAX)		NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]					DATETIME			NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]					NVARCHAR(MAX)		NULL,
	[Audit_ModifiedOn]					DATETIME			NULL,
	[Audit_DeletedBy]					NVARCHAR(MAX)		NULL,
	[Audit_DeletedOn]					DATETIME			NULL,
	[Audit_IsDeleted]					BIT					NOT NULL	DEFAULT 0,
	[Audit_VersionId]					UNIQUEIDENTIFIER	NOT NULL	DEFAULT NEWID(), 
    
)