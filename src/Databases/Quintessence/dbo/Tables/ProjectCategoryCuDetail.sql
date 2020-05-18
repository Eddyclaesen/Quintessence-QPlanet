CREATE TABLE [dbo].[ProjectCategoryCuDetail] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProjectCategoryCuDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryCuDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

