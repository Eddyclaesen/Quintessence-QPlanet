CREATE TABLE [dbo].[ProjectCategoryDetailType1](
	[Id]								UNIQUEIDENTIFIER	NOT NULL,
	[Name]								NVARCHAR(MAX)		NOT NULL,
	[Description]						NVARCHAR(MAX)		NULL,
	[SurveyPlanningId]					INT					NOT NULL,
	[MailTextStandalone]				NVARCHAR(MAX)		NOT NULL,
	[MailTextIntegrated]				NVARCHAR(MAX)		NOT NULL
)