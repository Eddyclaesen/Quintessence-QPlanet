CREATE TABLE [dbo].[HistoryIdMapping](
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[QuintessenceId]		UNIQUEIDENTIFIER	NOT NULL,
	[QuintessenceEntity]	NVARCHAR(MAX)		NOT NULL,
	[ExternalId]			NVARCHAR(MAX)		NOT NULL,	
	[ExternalSystem]		NVARCHAR(MAX)		NOT NULL
)