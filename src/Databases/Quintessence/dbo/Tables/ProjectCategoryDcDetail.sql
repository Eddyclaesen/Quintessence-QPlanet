CREATE TABLE [dbo].[ProjectCategoryDcDetail] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ScoringTypeCode]     INT              NOT NULL,
    [SimulationRemarks]   TEXT             NULL,
    [SimulationContextId] UNIQUEIDENTIFIER NULL,
    [MatrixRemarks]       TEXT             NULL,
    CONSTRAINT [PK_ProjectCategoryDcDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDcDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

