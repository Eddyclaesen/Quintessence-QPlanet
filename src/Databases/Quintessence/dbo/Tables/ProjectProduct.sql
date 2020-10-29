﻿CREATE TABLE [dbo].[ProjectProduct] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]           UNIQUEIDENTIFIER NOT NULL,
    [ProductTypeId]       UNIQUEIDENTIFIER NOT NULL,
    [InvoiceAmount]       DECIMAL (18, 2)  NULL,
    [InvoiceStatusCode]   INT              NOT NULL,
    [InvoicedDate]        DATETIME         NULL,
    [InvoiceRemarks]      NVARCHAR (MAX)   NULL,
    [PurchaseOrderNumber] NVARCHAR (MAX)   NULL,
    [InvoiceNumber]       NVARCHAR (MAX)   NULL,
    [Description]         NVARCHAR (MAX)   NULL,
    [ProposalId]          UNIQUEIDENTIFIER NULL,
    [Audit_CreatedBy]     NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]     DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]    NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]    DATETIME         NULL,
    [Audit_DeletedBy]     NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]     DATETIME         NULL,
    [Audit_IsDeleted]     BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Deadline]            DATETIME         NULL,
    [NoInvoice]           BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProjectProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectProduct_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([Id]),
    CONSTRAINT [FK_ProjectProduct_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);



