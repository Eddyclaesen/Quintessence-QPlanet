CREATE TABLE [dbo].[ProjectRole2DictionaryLevel] (
    [ProjectRoleId]     UNIQUEIDENTIFIER NOT NULL,
    [DictionaryLevelId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProjectRole2DictionaryLevel] PRIMARY KEY NONCLUSTERED ([ProjectRoleId] ASC, [DictionaryLevelId] ASC)
);

