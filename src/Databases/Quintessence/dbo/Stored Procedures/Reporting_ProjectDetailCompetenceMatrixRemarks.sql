CREATE PROCEDURE Reporting_ProjectDetailCompetenceMatrixRemarks
	@ProjectId	UNIQUEIDENTIFIER
AS
BEGIN	
	SELECT		CASE ([ProjectTypeCategoryView].[Code])
					WHEN 'AC' THEN [ProjectCategoryAcDetailView].[MatrixRemarks]
					WHEN 'DC' THEN [ProjectCategoryDcDetailView].[MatrixRemarks]
					WHEN 'EA' THEN [ProjectCategoryEaDetailView].[MatrixRemarks]
					WHEN 'FA' THEN [ProjectCategoryFaDetailView].[MatrixRemarks]
					WHEN 'FD' THEN [ProjectCategoryFdDetailView].[MatrixRemarks]
					WHEN 'PS' THEN [ProjectCategoryPsDetailView].[MatrixRemarks]
					WHEN 'SO' THEN [ProjectCategorySoDetailView].[MatrixRemarks]
				END	AS	MatrixRemarks

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
	
	WHERE		[ProjectCategoryDetailView].[ProjectId] = @ProjectId
END