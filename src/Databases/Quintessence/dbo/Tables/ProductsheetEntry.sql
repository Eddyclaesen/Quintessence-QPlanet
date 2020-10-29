﻿CREATE TABLE [dbo].[ProductsheetEntry] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (MAX)   NOT NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]           UNIQUEIDENTIFIER NOT NULL,
    [ProjectPlanPhaseId]  UNIQUEIDENTIFIER NOT NULL,
    [ProductId]           UNIQUEIDENTIFIER NOT NULL,
    [Quantity]            INT              NOT NULL,
    [InvoiceAmount]       DECIMAL (18, 2)  NULL,
    [InvoiceStatusCode]   INT              NOT NULL,
    [InvoicedDate]        DATETIME         NULL,
    [InvoiceRemarks]      NVARCHAR (MAX)   NULL,
    [PurchaseOrderNumber] NVARCHAR (MAX)   NULL,
    [InvoiceNumber]       NVARCHAR (MAX)   NULL,
    [Date]                DATETIME         NOT NULL,
    [Description]         TEXT             NULL,
    [ProposalId]          UNIQUEIDENTIFIER NULL,
    [Audit_CreatedBy]     NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]     DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]    NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]    DATETIME         NULL,
    [Audit_DeletedBy]     NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]     DATETIME         NULL,
    [Audit_IsDeleted]     BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TProductsheetEntry] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductsheetEntry_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_ProductsheetEntry_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_ProductsheetEntry_ProjectPlanPhase] FOREIGN KEY ([ProjectPlanPhaseId]) REFERENCES [dbo].[ProjectPlanPhase] ([Id]),
    CONSTRAINT [FK_ProductsheetEntry_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);



