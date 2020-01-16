CREATE TABLE [dbo].[JobSchedule] (
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[JobDefinitionId]		UNIQUEIDENTIFIER	NOT NULL,
	[StartTime]				TIME				NOT NULL,
	[EndTime]				TIME				NOT NULL,
	[Interval]				INT					NOT NULL,
	[IsEnabled]				BIT					NOT NULL
)