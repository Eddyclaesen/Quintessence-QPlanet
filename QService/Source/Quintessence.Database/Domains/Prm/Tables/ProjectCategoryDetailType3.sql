CREATE TABLE [dbo].[ProjectCategoryDetailType3](
	[Id]								UNIQUEIDENTIFIER	NOT NULL,
	[Name]								NVARCHAR(MAX)		NOT NULL,
	[Description]						NVARCHAR(MAX)		NULL,
	[SurveyPlanningId]					INT					NOT NULL,
	[IncludeInCandidateReport]			BIT					NOT NULL,
	[MailTextStandalone]				NVARCHAR(MAX)		NOT NULL,
	[MailTextIntegrated]				NVARCHAR(MAX)		NOT NULL
)