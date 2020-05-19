CREATE TABLE [dbo].[ProjectCandidate] (
    [Id]                                UNIQUEIDENTIFIER NOT NULL,
    [CandidateId]                       UNIQUEIDENTIFIER NOT NULL,
    [CrmCandidateAppointmentId]         INT              NOT NULL,
    [CrmCandidateInfoId]                INT              NOT NULL,
    [ProjectId]                         UNIQUEIDENTIFIER NOT NULL,
    [ReportDeadline]                    DATETIME         NOT NULL,
    [ReportLanguageId]                  INT              NOT NULL,
    [ReportReviewerId]                  UNIQUEIDENTIFIER NULL,
    [ReportStatusId]                    INT              NOT NULL,
    [IsCancelled]                       BIT              CONSTRAINT [DF__tmp_ms_xx__IsCan__4F52B2DB] DEFAULT ((0)) NOT NULL,
    [CancelledDate]                     DATETIME         NULL,
    [CancelledAppointmentDate]          DATETIME         NULL,
    [CancelledReason]                   NVARCHAR (MAX)   NULL,
    [InvoiceAmount]                     DECIMAL (18, 2)  NULL,
    [InvoiceStatusCode]                 INT              NOT NULL,
    [InvoicedDate]                      DATETIME         NULL,
    [InvoiceRemarks]                    NVARCHAR (MAX)   NULL,
    [PurchaseOrderNumber]               NVARCHAR (MAX)   NULL,
    [InvoiceNumber]                     NVARCHAR (MAX)   NULL,
    [Remarks]                           NVARCHAR (MAX)   NULL,
    [ScoringCoAssessorId]               UNIQUEIDENTIFIER NULL,
    [IsAccompaniedByCustomer]           BIT              CONSTRAINT [DF__tmp_ms_xx__IsAcc__5046D714] DEFAULT ((0)) NOT NULL,
    [FollowUpDone]                      BIT              CONSTRAINT [DF__tmp_ms_xx__Follo__513AFB4D] DEFAULT ((0)) NOT NULL,
    [OrderConfirmationSentDate]         DATETIME         NULL,
    [OrderConfirmationReceivedDate]     DATETIME         NULL,
    [InvitationSentDate]                DATETIME         NULL,
    [LeafletSentDate]                   DATETIME         NULL,
    [ReportMailSentDate]                DATETIME         NULL,
    [DossierReadyDate]                  DATETIME         NULL,
    [ReportDeadlineDone]                BIT              CONSTRAINT [DF__tmp_ms_xx__Repor__522F1F86] DEFAULT ((0)) NOT NULL,
    [OrderConfirmationSentDateDone]     BIT              CONSTRAINT [DF__tmp_ms_xx__Order__532343BF] DEFAULT ((0)) NOT NULL,
    [OrderConfirmationReceivedDateDone] BIT              CONSTRAINT [DF__tmp_ms_xx__Order__541767F8] DEFAULT ((0)) NOT NULL,
    [InvitationSentDateDone]            BIT              CONSTRAINT [DF__tmp_ms_xx__Invit__550B8C31] DEFAULT ((0)) NOT NULL,
    [LeafletSentDateDone]               BIT              CONSTRAINT [DF__tmp_ms_xx__Leafl__55FFB06A] DEFAULT ((0)) NOT NULL,
    [ReportMailSentDateDone]            BIT              CONSTRAINT [DF__tmp_ms_xx__Repor__56F3D4A3] DEFAULT ((0)) NOT NULL,
    [DossierReadyDateDone]              BIT              CONSTRAINT [DF__tmp_ms_xx__Dossi__57E7F8DC] DEFAULT ((0)) NOT NULL,
    [Extra1]                            NVARCHAR (MAX)   NULL,
    [Extra2]                            NVARCHAR (MAX)   NULL,
    [Extra1Done]                        BIT              CONSTRAINT [DF__tmp_ms_xx__Extra__58DC1D15] DEFAULT ((0)) NOT NULL,
    [Extra2Done]                        BIT              CONSTRAINT [DF__tmp_ms_xx__Extra__59D0414E] DEFAULT ((0)) NOT NULL,
    [ProposalId]                        UNIQUEIDENTIFIER NULL,
    [Audit_CreatedBy]                   NVARCHAR (MAX)   CONSTRAINT [DF__tmp_ms_xx__Audit__5AC46587] DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]                   DATETIME         CONSTRAINT [DF__tmp_ms_xx__Audit__5BB889C0] DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]                  NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]                  DATETIME         NULL,
    [Audit_DeletedBy]                   NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]                   DATETIME         NULL,
    [Audit_IsDeleted]                   BIT              CONSTRAINT [DF__tmp_ms_xx__Audit__5CACADF9] DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]                   UNIQUEIDENTIFIER CONSTRAINT [DF__tmp_ms_xx__Audit__5DA0D232] DEFAULT (newid()) NOT NULL,
    [InternalCandidate]                 BIT              CONSTRAINT [DF_ProjectCandidate_InternalCandidate] DEFAULT ((0)) NOT NULL,
    [OnlineAssessment]                  BIT              CONSTRAINT [DF_ProjectCandidate_OnlineAssessment] DEFAULT ((0)) NULL,
    [FinancialEntityId]                 UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProjectCandidate] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCandidate_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([Id]),
    CONSTRAINT [FK_ProjectCandidate_Invoicing_FinancialEntity] FOREIGN KEY ([FinancialEntityId]) REFERENCES [dbo].[Invoicing_FinancialEntity] ([Id]),
    CONSTRAINT [FK_ProjectCandidate_Language] FOREIGN KEY ([ReportLanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_ProjectCandidate_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_ProjectCandidate_ReportStatus] FOREIGN KEY ([ReportStatusId]) REFERENCES [dbo].[ReportStatus] ([Id])
);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER trgProjectCandidate_Cancel
   ON  [dbo].[ProjectCandidate]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	IF UPDATE(ISCancelled)
	BEGIN
	  IF (select IsCancelled from inserted) = 1
		IF EXISTS (select Id
			from DataWarehouse.dbo.QuintessencePortalSubmissionCandidate
			where QplanetCandidateInfoId = (select CrmCandidateInfoId from inserted))
			BEGIN
				INSERT INTO DataWarehouse.dbo.MailQueue values ('Portal_Submission_Cancellation',(select CrmCandidateInfoId from inserted),GETDATE(),NULL,'',0,NULL)
			END
	END
END

GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[trgProjectCandidate] ON [dbo].[ProjectCandidate]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS (SELECT Id from DataWarehouse.dbo.QuintessencePortalSubmissionCandidate where Code COLLATE DATABASE_DEFAULT = (select Code from quintessence.dbo.CrmReplicationAppointment where Id = (select CrmCandidateAppointmentId from inserted)))
		BEGIN
			UPDATE DataWarehouse.dbo.QuintessencePortalSubmissionCandidate
			SET QplanetCandidateInfoId = (SELECT CrmCandidateInfoId from inserted)
			WHERE Code COLLATE DATABASE_DEFAULT = (select Code from quintessence.dbo.CrmReplicationAppointment where Id = (select CrmCandidateAppointmentId from inserted))
		END

	IF EXISTS (select p.Id FROM [Quintessence].[dbo].[ProjectCategoryDetail] p
				left join Quintessence.dbo.ProjectTypeCategoryView pp on p.ProjectTypeCategoryId = pp.id	
				where p.ProjectId = (SELECT ProjectId from inserted)
				and pp.Name = 'Motivation Questionnaire')
		BEGIN
			UPDATE Quintessence.dbo.ProjectCandidateView
			SET Remarks = 'MQ: https://drive.eu.shl.com/?linkid=KZi4zr6VWWWnGRIjTt5Bkr0MBGAumPUX8IDODS6ncAGbVD1eAbb4xg'
			WHERE Id = (SELECT Id from inserted)
		END
END
