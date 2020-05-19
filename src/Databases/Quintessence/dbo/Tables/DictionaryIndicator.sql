CREATE TABLE [dbo].[DictionaryIndicator] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [DictionaryLevelId] UNIQUEIDENTIFIER NOT NULL,
    [Name]              NVARCHAR (MAX)   NOT NULL,
    [Order]             INT              NOT NULL,
    [IsStandard]        BIT              NULL,
    [IsDistinctive]     BIT              NULL,
    [Audit_CreatedBy]   NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]  NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]  DATETIME         NULL,
    [Audit_DeletedBy]   NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]   DATETIME         NULL,
    [Audit_IsDeleted]   BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [LegacyId]          INT              NOT NULL,
    [Color]             NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_DictionaryIndicator] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DictionaryIndicator_DictionaryLevel] FOREIGN KEY ([DictionaryLevelId]) REFERENCES [dbo].[DictionaryLevel] ([Id])
);

