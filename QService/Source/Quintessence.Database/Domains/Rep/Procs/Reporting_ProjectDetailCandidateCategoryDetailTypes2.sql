CREATE PROCEDURE Reporting_ProjectDetailCandidateCategoryDetailTypes2
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeName]		AS	[ProjectCategoryDetailTypeName],
				[ProjectCandidateCategoryDetailType2View].[Deadline]							AS	[ScheduledDate],
				[ProjectCategoryDetailType2View].[SurveyPlanningId]								AS	[SurveyPlanningId],
				[SurveyPlanning].[Text]															AS	[SurveyPlanningText]

	FROM		[ProjectCandidateCategoryDetailType2View]

	INNER JOIN	[ProjectCategoryDetailType2View]
		ON		[ProjectCategoryDetailType2View].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[SurveyPlanning]
		ON		[SurveyPlanning].[Id] = [ProjectCategoryDetailType2View].[SurveyPlanningId]

	WHERE		[ProjectCandidateCategoryDetailType2View].[ProjectCandidateId] = @ProjectCandidateId
END
GO
