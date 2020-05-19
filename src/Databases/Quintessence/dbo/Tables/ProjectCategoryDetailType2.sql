CREATE TABLE [dbo].[ProjectCategoryDetailType2] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [Description]      NVARCHAR (MAX)   NULL,
    [SurveyPlanningId] INT              NOT NULL,
    CONSTRAINT [PK_ProjectCategoryDetailType2] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetailType2_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetailType2_SurveyPlanning] FOREIGN KEY ([SurveyPlanningId]) REFERENCES [dbo].[SurveyPlanning] ([Id])
);

