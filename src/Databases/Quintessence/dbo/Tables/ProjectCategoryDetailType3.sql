CREATE TABLE [dbo].[ProjectCategoryDetailType3] (
    [Id]                       UNIQUEIDENTIFIER NOT NULL,
    [Name]                     NVARCHAR (MAX)   NOT NULL,
    [Description]              NVARCHAR (MAX)   NULL,
    [SurveyPlanningId]         INT              NOT NULL,
    [IncludeInCandidateReport] BIT              NOT NULL,
    [MailTextStandalone]       NVARCHAR (MAX)   NOT NULL,
    [MailTextIntegrated]       NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_ProjectCategoryDetailType3] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetailType3_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetailType3_SurveyPlanning] FOREIGN KEY ([SurveyPlanningId]) REFERENCES [dbo].[SurveyPlanning] ([Id])
);

