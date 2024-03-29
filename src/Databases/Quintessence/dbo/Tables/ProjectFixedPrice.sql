﻿CREATE TABLE [dbo].[ProjectFixedPrice] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]           UNIQUEIDENTIFIER NOT NULL,
    [Amount]              DECIMAL (18, 2)  NOT NULL,
    [InvoiceStatusCode]   INT              NOT NULL,
    [InvoicedDate]        DATETIME         NULL,
    [InvoiceRemarks]      NVARCHAR (MAX)   NULL,
    [PurchaseOrderNumber] NVARCHAR (MAX)   NULL,
    [InvoiceNumber]       NVARCHAR (MAX)   NULL,
    [Deadline]            DATETIME         NOT NULL,
    [ProposalId]          UNIQUEIDENTIFIER NULL,
    [Audit_CreatedBy]     NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]     DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]    NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]    DATETIME         NULL,
    [Audit_DeletedBy]     NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]     DATETIME         NULL,
    [Audit_IsDeleted]     BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectFixedPrice] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectFixedPrice_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);



