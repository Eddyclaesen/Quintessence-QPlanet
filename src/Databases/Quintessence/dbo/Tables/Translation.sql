CREATE TABLE [dbo].[Translation] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]       INT              NOT NULL,
    [TableId]          UNIQUEIDENTIFIER NOT NULL,
    [TableName]        NVARCHAR (MAX)   NOT NULL,
    [Text]             TEXT             NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Translation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Translation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);

