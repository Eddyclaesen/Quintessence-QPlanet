CREATE TABLE [dbo].[OfficeTranslation] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [OfficeId]   INT            NOT NULL,
    [LanguageId] INT            NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_OfficeTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OfficeTranslation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_OfficeTranslation_Office] FOREIGN KEY ([OfficeId]) REFERENCES [dbo].[Office] ([Id])
);

