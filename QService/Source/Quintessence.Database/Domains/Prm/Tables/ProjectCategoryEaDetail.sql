CREATE TABLE [dbo].[ProjectCategoryEaDetail](
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[ScoringTypeCode]		INT					NOT NULL,
	[SimulationRemarks]		TEXT				NULL,
	[SimulationContextId]	UNIQUEIDENTIFIER	NULL,
	[MatrixRemarks]			TEXT				NULL
)