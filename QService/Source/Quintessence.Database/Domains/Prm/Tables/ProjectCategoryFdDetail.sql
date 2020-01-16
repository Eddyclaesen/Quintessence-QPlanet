CREATE TABLE [dbo].[ProjectCategoryFdDetail](
	[Id]					UNIQUEIDENTIFIER	NOT NULL,
	[ScoringTypeCode]		INT					NOT NULL,
	[SimulationRemarks]		TEXT				NULL,
	[SimulationContextId]	UNIQUEIDENTIFIER	NULL,
	[MatrixRemarks]			TEXT				NULL,
	[ProjectRoleId]			UNIQUEIDENTIFIER	NULL
)