CREATE TABLE [dbo].[ProjectCategoryEaDetail] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ScoringTypeCode]     INT              NOT NULL,
    [SimulationRemarks]   TEXT             NULL,
    [SimulationContextId] UNIQUEIDENTIFIER NULL,
    [MatrixRemarks]       TEXT             NULL,
    CONSTRAINT [PK_ProjectCategoryEaDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryEaDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

