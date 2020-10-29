﻿CREATE TABLE [dbo].[ProjectCandidateCategoryDetailType2] (
    [Id]                           UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]           UNIQUEIDENTIFIER NOT NULL,
    [ProjectCategoryDetailType2Id] UNIQUEIDENTIFIER NOT NULL,
    [Deadline]                     DATETIME         NULL,
    [InvoiceAmount]                DECIMAL (18, 2)  NOT NULL,
    [InvoiceStatusCode]            INT              NOT NULL,
    [InvoicedDate]                 DATETIME         NULL,
    [InvoiceRemarks]               NVARCHAR (MAX)   NULL,
    [PurchaseOrderNumber]          NVARCHAR (MAX)   NULL,
    [InvoiceNumber]                NVARCHAR (MAX)   NULL,
    [InvitationSentDate]           DATETIME         NULL,
    [DossierReadyDate]             DATETIME         NULL,
    [InvitationSentDateDone]       BIT              DEFAULT ((0)) NOT NULL,
    [DossierReadyDateDone]         BIT              DEFAULT ((0)) NOT NULL,
    [FollowUpDone]                 BIT              DEFAULT ((0)) NOT NULL,
    [Extra1]                       NVARCHAR (MAX)   NULL,
    [Extra2]                       NVARCHAR (MAX)   NULL,
    [Extra1Done]                   BIT              DEFAULT ((0)) NOT NULL,
    [Extra2Done]                   BIT              DEFAULT ((0)) NOT NULL,
    [ProposalId]                   UNIQUEIDENTIFIER NULL,
    [Audit_CreatedBy]              NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]              DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]             NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]             DATETIME         NULL,
    [Audit_DeletedBy]              NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]              DATETIME         NULL,
    [Audit_IsDeleted]              BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectCandidateCategoryDetailType2] PRIMARY KEY CLUSTERED ([ProjectCandidateId] ASC, [ProjectCategoryDetailType2Id] ASC),
    CONSTRAINT [FK_ProjectCandidateCategoryDetailType2_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id]),
    CONSTRAINT [FK_ProjectCandidateCategoryDetailType2_ProjectCategoryDetailType2] FOREIGN KEY ([ProjectCategoryDetailType2Id]) REFERENCES [dbo].[ProjectCategoryDetailType2] ([Id])
);



