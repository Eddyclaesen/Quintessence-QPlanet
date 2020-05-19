CREATE TABLE [dbo].[ProjectCategoryDetailType1] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Name]               NVARCHAR (MAX)   NOT NULL,
    [Description]        NVARCHAR (MAX)   NULL,
    [SurveyPlanningId]   INT              NOT NULL,
    [MailTextStandalone] NVARCHAR (MAX)   NOT NULL,
    [MailTextIntegrated] NVARCHAR (MAX)   NOT NULL,
    [WithReport]         BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProjectCategoryDetailType1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectCategoryDetailType1_ProjectCategoryDetail] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectCategoryDetail] ([Id]),
    CONSTRAINT [FK_ProjectCategoryDetailType1_SurveyPlanning] FOREIGN KEY ([SurveyPlanningId]) REFERENCES [dbo].[SurveyPlanning] ([Id])
);

