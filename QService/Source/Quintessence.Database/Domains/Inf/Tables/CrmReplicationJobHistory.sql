CREATE TABLE [dbo].[CrmReplicationJobHistory] (
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[CrmReplicationJobId]	UNIQUEIDENTIFIER	NOT NULL,
	[StartDate]				DATETIME			NOT NULL,
	[EndDate]				DATETIME			NULL,
	[Succeeded]				BIT					NULL
)