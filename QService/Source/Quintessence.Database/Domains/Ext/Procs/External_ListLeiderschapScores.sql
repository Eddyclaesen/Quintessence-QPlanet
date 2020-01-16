CREATE PROCEDURE [dbo].[External_ListLeiderschapScores]
	@ProjectCandidateId		UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE	@Password	AS	VARCHAR(6)

	SELECT		@Password = [ProjectCandidateCategoryDetailType3View].[LoginCode]

	FROM		[ProjectCandidateCategoryDetailType3View]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[Code] = 'LEIDERSTIJ'

	WHERE		[ProjectCandidateCategoryDetailType3View].[ProjectCandidateId] = @ProjectCandidateId
	
	EXECUTE		[$(superoffice7server)].[$(Superoffice7)].[dbo].[QPlanet_ListLeiderschapScores]		@Password
END
GO
