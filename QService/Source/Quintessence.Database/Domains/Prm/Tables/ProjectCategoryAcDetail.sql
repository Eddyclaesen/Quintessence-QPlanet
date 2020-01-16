CREATE TABLE [dbo].[ProjectCategoryAcDetail](
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[ScoringTypeCode]		INT					NOT NULL,
	[SimulationContextId]	UNIQUEIDENTIFIER	NULL,
	[SimulationRemarks]		TEXT				NULL,
	[MatrixRemarks]			TEXT				NULL
)