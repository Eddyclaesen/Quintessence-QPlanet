CREATE TABLE [dbo].[ProjectType2ProjectTypeCategory] (
    [ProjectTypeId]         UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [IsMain]                BIT              DEFAULT ((0)) NULL,
    CONSTRAINT [PK_ProjectType2ProjectTypeCategory] PRIMARY KEY NONCLUSTERED ([ProjectTypeId] ASC, [ProjectTypeCategoryId] ASC),
    CONSTRAINT [FK_ProjectType2ProjectCategory_ProjectCategory] FOREIGN KEY ([ProjectTypeCategoryId]) REFERENCES [dbo].[ProjectTypeCategory] ([Id]),
    CONSTRAINT [FK_ProjectType2ProjectTypeCategory_ProjectType] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectType] ([Id])
);

