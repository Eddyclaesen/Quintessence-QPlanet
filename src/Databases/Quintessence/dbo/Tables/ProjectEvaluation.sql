CREATE TABLE [dbo].[ProjectEvaluation] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [CrmProjectId]     INT              NOT NULL,
    [LessonsLearned]   NVARCHAR (MAX)   NULL,
    [Evaluation]       NVARCHAR (MAX)   NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectEvaluation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

