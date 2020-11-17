CREATE PROCEDURE [dbo].[Context_GetIdByProgramComponentId]
	@programComponentId UNIQUEIDENTIFIER
AS
	
SET NOCOUNT ON;

WITH [Contexts] AS
(
	SELECT
		[Id],
		SimulationContextId, 'AC' AS [Type]
	FROM [dbo].[ProjectCategoryAcDetailView] WITH (NOLOCK)
	
	UNION ALL

    SELECT
		[Id],
		SimulationContextId, 'CA' AS [Type]
	FROM [dbo].[ProjectCategoryCaDetailView] WITH (NOLOCK)
    
	UNION ALL

    SELECT 
		[Id],
		SimulationContextId, 'DC' AS [Type]
	FROM [dbo].[ProjectCategoryDcDetailView] WITH (NOLOCK)

    UNION ALL

    SELECT 
		[Id],
		SimulationContextId, 'EA' AS [Type]
	FROM [dbo].[ProjectCategoryEaDetailView] WITH (NOLOCK)
    
	UNION ALL

    SELECT
		[Id],
		SimulationContextId, 'FA' AS [Type]
	FROM [dbo].[ProjectCategoryFaDetailView] WITH (NOLOCK)
    
	UNION ALL
    
	SELECT 
		[Id],
		SimulationContextId, 'FD' AS [Type]
	FROM [dbo].[ProjectCategoryFdDetailView] WITH (NOLOCK)
    
	UNION ALL
    
	SELECT 
		[Id],
		SimulationContextId, 'PS' AS [Type]
	FROM [dbo].[ProjectCategoryPsDetailView] WITH (NOLOCK)
    
	UNION ALL
    
	SELECT 
		[Id],
		SimulationContextId, 'PS' AS [Type]
	FROM [dbo].[ProjectCategorySoDetailView] WITH (NOLOCK)
)

SELECT C.[SimulationContextId]
FROM dbo.[ProgramComponent] PC1 WITH (NOLOCK)
	INNER JOIN  dbo.[ProjectCandidate] PC2 WITH (NOLOCK)
		ON PC2.[Id] = PC1.[ProjectCandidateId]
	INNER JOIN [dbo].[ProjectCategoryDetailView] PCDV WITH (NOLOCK)
		ON PCDV.[ProjectId] = PC2.[ProjectId] 
	INNER JOIN [dbo].[ProjectTypeCategoryView] PTC  WITH (NOLOCK)
		ON PCDV.ProjectTypeCategoryId = PTC.[Id]
	INNER JOIN [Contexts] C      
		ON PTC.[Code] = C.[Type]
			AND C.[Id] = PCDV.[Id]
WHERE PC1.[Id] = @programComponentId
