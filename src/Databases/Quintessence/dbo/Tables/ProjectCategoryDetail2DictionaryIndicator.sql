CREATE TABLE [dbo].[ProjectCategoryDetail2DictionaryIndicator] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [ProjectCategoryDetailId] UNIQUEIDENTIFIER NOT NULL,
    [DictionaryIndicatorId]   UNIQUEIDENTIFIER NOT NULL,
    [IsDefinedByRole]         BIT              DEFAULT ((0)) NOT NULL,
    [IsStandard]              BIT              NULL,
    [IsDistinctive]           BIT              NULL,
    CONSTRAINT [PK_ProjectCategoryDetail2DictionaryIndicator] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetail2DictionaryIndicator_DictionaryIndicatory] FOREIGN KEY ([DictionaryIndicatorId]) REFERENCES [dbo].[DictionaryIndicator] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetail2DictionaryIndicator_ProjectCategoryDetail] FOREIGN KEY ([ProjectCategoryDetailId]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

