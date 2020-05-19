﻿CREATE TABLE [dbo].[ReportDefinition] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ReportTypeId]     INT              NOT NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [ExportAsXml]      BIT              DEFAULT ((0)) NULL,
    [ExportAsCsvl]     BIT              DEFAULT ((0)) NULL,
    [ExportAsImg]      BIT              DEFAULT ((0)) NULL,
    [ExportAsPdf]      BIT              DEFAULT ((0)) NULL,
    [ExportAsMhtml]    BIT              DEFAULT ((0)) NULL,
    [ExportAsHtml4]    BIT              DEFAULT ((0)) NULL,
    [ExportAsHtml32]   BIT              DEFAULT ((0)) NULL,
    [ExportAsExcel]    BIT              DEFAULT ((0)) NULL,
    [ExportAsWord]     BIT              DEFAULT ((0)) NULL,
    [Location]         NVARCHAR (MAX)   NOT NULL,
    [IsActive]         BIT              DEFAULT ((1)) NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ReportDefinition] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportDefinition_ReportType] FOREIGN KEY ([ReportTypeId]) REFERENCES [dbo].[ReportType] ([Id])
);

