CREATE TABLE [dbo].[ProjectCategoryFdDetail] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ScoringTypeCode]     INT              NOT NULL,
    [SimulationRemarks]   TEXT             NULL,
    [SimulationContextId] UNIQUEIDENTIFIER NULL,
    [MatrixRemarks]       TEXT             NULL,
    [ProjectRoleId]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProjectCategoryFdDetail] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryFdDetail_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id])
);

