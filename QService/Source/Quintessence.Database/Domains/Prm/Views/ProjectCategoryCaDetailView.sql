CREATE VIEW [dbo].[ProjectCategoryCaDetailView] AS
	SELECT		[ProjectCategoryCaDetail].[Id],
				[ProjectCategoryCaDetail].[ScoringTypeCode],
				[ProjectCategoryCaDetail].[SimulationRemarks],
				[ProjectCategoryCaDetail].[SimulationContextId],
				[ProjectCategoryCaDetail].[MatrixRemarks]

	FROM		[ProjectCategoryCaDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryCaDetail].[Id]
