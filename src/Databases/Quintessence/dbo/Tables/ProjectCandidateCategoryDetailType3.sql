﻿CREATE TABLE [dbo].[ProjectCandidateCategoryDetailType3] (
    [Id]                           UNIQUEIDENTIFIER NOT NULL,
    [ProjectCandidateId]           UNIQUEIDENTIFIER NOT NULL,
    [ProjectCategoryDetailType3Id] UNIQUEIDENTIFIER NOT NULL,
    [Deadline]                     DATETIME         NULL,
    [LoginCode]                    NVARCHAR (MAX)   NULL,
    [InvoiceAmount]                DECIMAL (18, 2)  NOT NULL,
    [InvoiceStatusCode]            INT              NOT NULL,
    [InvoiceRemarks]               NVARCHAR (MAX)   NULL,
    [PurchaseOrderNumber]          NVARCHAR (MAX)   NULL,
    [InvoiceNumber]                NVARCHAR (MAX)   NULL,
    [InvoicedDate]                 DATETIME         NULL,
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
    [FinancialEntityId]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProjectCandidateCategoryDetailType3] PRIMARY KEY CLUSTERED ([ProjectCandidateId] ASC, [ProjectCategoryDetailType3Id] ASC),
    CONSTRAINT [FK_ProjectCandidateCategoryDetailType3_Invoicing_FinancialEntity] FOREIGN KEY ([FinancialEntityId]) REFERENCES [dbo].[Invoicing_FinancialEntity] ([Id]),
    CONSTRAINT [FK_ProjectCandidateCategoryDetailType3_ProjectCandidate] FOREIGN KEY ([ProjectCandidateId]) REFERENCES [dbo].[ProjectCandidate] ([Id]),
    CONSTRAINT [FK_ProjectCandidateCategoryDetailType3_ProjectCategoryDetailType3] FOREIGN KEY ([ProjectCategoryDetailType3Id]) REFERENCES [dbo].[ProjectCategoryDetailType3] ([Id])
);

