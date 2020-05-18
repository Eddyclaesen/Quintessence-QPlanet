﻿CREATE TABLE [dbo].[Project] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [Name]                  NVARCHAR (100)   NOT NULL,
    [ProjectTypeId]         UNIQUEIDENTIFIER NOT NULL,
    [ContactId]             INT              NOT NULL,
    [ProjectManagerId]      UNIQUEIDENTIFIER NULL,
    [CoProjectManagerId]    UNIQUEIDENTIFIER NULL,
    [CustomerAssistantId]   UNIQUEIDENTIFIER NULL,
    [ProposalId]            UNIQUEIDENTIFIER NULL,
    [PricingModelId]        INT              NOT NULL,
    [StatusCode]            INT              NOT NULL,
    [Remarks]               TEXT             NULL,
    [DepartmentInformation] TEXT             NULL,
    [Audit_CreatedBy]       NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]       DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]      NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]      DATETIME         NULL,
    [Audit_DeletedBy]       NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]       DATETIME         NULL,
    [Audit_IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [LegacyId]              INT              NULL,
    [Confidential]          BIT              CONSTRAINT [DF_Project_Confidential] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Project_PricingModel] FOREIGN KEY ([PricingModelId]) REFERENCES [dbo].[PricingModel] ([Id]),
    CONSTRAINT [FK_Project_ProjectType] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectType] ([Id])
);

