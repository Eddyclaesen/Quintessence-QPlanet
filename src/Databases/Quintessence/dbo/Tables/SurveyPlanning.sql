CREATE TABLE [dbo].[SurveyPlanning] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Text] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SurveyPlanning] PRIMARY KEY CLUSTERED ([Id] ASC)
);

