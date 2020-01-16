CREATE TABLE [dbo].[ProjectPlanPhaseActivity](
	[Id]						UNIQUEIDENTIFIER		NOT NULL,
	[ActivityId]				UNIQUEIDENTIFIER		NOT NULL,
	[ProfileId]					UNIQUEIDENTIFIER		NOT NULL,
	[Duration]					DECIMAL(18,2)			NOT NULL,
	[Notes]						NVARCHAR(MAX)			NULL,
)