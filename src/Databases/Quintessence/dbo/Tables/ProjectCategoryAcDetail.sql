CREATE TABLE [dbo].[ProjectCategoryAcDetail] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ScoringTypeCode]     INT              NOT NULL,
    [SimulationContextId] UNIQUEIDENTIFIER NULL,
    [SimulationRemarks]   TEXT             NULL,
    [MatrixRemarks]       TEXT             NULL,
    CONSTRAINT [PK_ProjectCategoryAcDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryAcDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

