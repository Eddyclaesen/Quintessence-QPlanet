CREATE TABLE [dbo].[ProjectCategoryCaDetail] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ScoringTypeCode]     INT              NOT NULL,
    [SimulationRemarks]   TEXT             NULL,
    [SimulationContextId] UNIQUEIDENTIFIER NULL,
    [MatrixRemarks]       TEXT             NULL,
    CONSTRAINT [PK_ProjectCategoryCaDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryCaDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

