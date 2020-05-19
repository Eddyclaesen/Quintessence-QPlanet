CREATE TABLE [dbo].[ProjectCategoryFaDetail] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ScoringTypeCode]     INT              NOT NULL,
    [SimulationRemarks]   TEXT             NULL,
    [SimulationContextId] UNIQUEIDENTIFIER NULL,
    [MatrixRemarks]       TEXT             NULL,
    [ProjectRoleId]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProjectCategoryFaDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryFaDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

