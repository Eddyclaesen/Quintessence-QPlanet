CREATE PROCEDURE Reporting_ProjectDetailSimulationRemarks
	@ProjectId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		CASE ([ProjectTypeCategoryView].[Code])
					WHEN 'AC' THEN [ProjectCategoryAcDetailView].[SimulationRemarks]
					WHEN 'DC' THEN [ProjectCategoryDcDetailView].[SimulationRemarks]
					WHEN 'EA' THEN [ProjectCategoryEaDetailView].[SimulationRemarks]
					WHEN 'FA' THEN [ProjectCategoryFaDetailView].[SimulationRemarks]
					WHEN 'FD' THEN [ProjectCategoryFdDetailView].[SimulationRemarks]
					WHEN 'PS' THEN [ProjectCategoryPsDetailView].[SimulationRemarks]
					WHEN 'SO' THEN [ProjectCategorySoDetailView].[SimulationRemarks]
					WHEN 'CA' THEN [ProjectCategoryCaDetailView].[SimulationRemarks]
				END	AS	SimulationRemarks

	FROM		[ProjectCategoryDetailView]
	
	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
	
	INNER JOIN	[ProjectType2ProjectTypeCategory]
		ON		[ProjectType2ProjectTypeCategory].[ProjectTypeCategoryId] = [ProjectTypeCategoryView].[Id]
		AND		[ProjectType2ProjectTypeCategory].[IsMain] = 1
	
	LEFT JOIN	[ProjectCategoryAcDetailView]
		ON		[ProjectCategoryAcDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategoryDcDetailView]
		ON		[ProjectCategoryDcDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategoryEaDetailView]
		ON		[ProjectCategoryEaDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategoryFaDetailView]
		ON		[ProjectCategoryFaDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategoryFdDetailView]
		ON		[ProjectCategoryFdDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategoryPsDetailView]
		ON		[ProjectCategoryPsDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	LEFT JOIN	[ProjectCategorySoDetailView]
		ON		[ProjectCategorySoDetailView].[Id] = [ProjectCategoryDetailView].[Id]

	LEFT JOIN	[ProjectCategoryCaDetailView]
		ON		[ProjectCategoryCaDetailView].[Id] = [ProjectCategoryDetailView].[Id]
	
	WHERE		[ProjectCategoryDetailView].[ProjectId] = @ProjectId
END