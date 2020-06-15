﻿CREATE TABLE [dbo].[EvaluationForm] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [CrmProjectId]         INT              NOT NULL,
    [FirstName]            NVARCHAR (MAX)   NOT NULL,
    [LastName]             NVARCHAR (MAX)   NOT NULL,
    [Gender]               CHAR (1)         NOT NULL,
    [Email]                NVARCHAR (MAX)   NOT NULL,
    [MailStatusTypeId]     INT              NOT NULL,
    [VerificationCode]     VARCHAR (6)      NOT NULL,
    [LanguageId]           INT              NOT NULL,
    [EvaluationFormTypeId] INT              NOT NULL,
    [IsCompleted]          BIT              DEFAULT ((0)) NOT NULL,
    [Audit_CreatedBy]      NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]      DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]     NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]     DATETIME         NULL,
    [Audit_DeletedBy]      NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]      DATETIME         NULL,
    [Audit_IsDeleted]      BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_EvaluationForm] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EvaluationForm_EvaluationFormType] FOREIGN KEY ([EvaluationFormTypeId]) REFERENCES [dbo].[EvaluationFormType] ([Id]),
    CONSTRAINT [FK_EvaluationForm_MailStatusType] FOREIGN KEY ([MailStatusTypeId]) REFERENCES [dbo].[MailStatusType] ([Id])
);

