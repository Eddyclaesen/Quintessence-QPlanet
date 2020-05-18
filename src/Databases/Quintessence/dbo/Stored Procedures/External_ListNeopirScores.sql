CREATE PROCEDURE [dbo].[External_ListNeopirScores]
	@ProjectCandidateId		UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE	@Password	AS	VARCHAR(6)
	DECLARE	@NormType	AS INT --3 = Male, 4 = Female

	SELECT		@Password = [ProjectCandidateCategoryDetailType3View].[LoginCode]

	FROM		[ProjectCandidateCategoryDetailType3View]

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]
		AND		[ProjectTypeCategoryView].[Code] = 'NEOPIR'

	WHERE		[ProjectCandidateCategoryDetailType3View].[ProjectCandidateId] = @ProjectCandidateId

	SELECT		@NormType = CASE WHEN [CandidateView].[Gender] = 'M' THEN 3 ELSE 4 END

	FROM		[ProjectCandidateView]

	INNER JOIN	[CandidateView]
		ON		[CandidateView].[Id] = [ProjectCandidateView].[CandidateId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
	
	EXECUTE		[Superoffice7].[dbo].[QPlanet_ListNeopirScoresReport]		@Password, 
																								@NormType
END