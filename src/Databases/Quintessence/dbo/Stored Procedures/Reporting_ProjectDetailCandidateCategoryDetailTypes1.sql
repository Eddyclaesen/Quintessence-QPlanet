CREATE PROCEDURE Reporting_ProjectDetailCandidateCategoryDetailTypes1
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeName]		AS	[ProjectCategoryDetailTypeName],
				[ProjectCandidateCategoryDetailType1View].[ScheduledDate]						AS	[ScheduledDate],
				[ProjectCategoryDetailType1View].[SurveyPlanningId]								AS	[SurveyPlanningId],
				[SurveyPlanning].[Text]															AS	[SurveyPlanningText]

	FROM		[ProjectCandidateCategoryDetailType1View]

	INNER JOIN	[ProjectCategoryDetailType1View]
		ON		[ProjectCategoryDetailType1View].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[SurveyPlanning]
		ON		[SurveyPlanning].[Id] = [ProjectCategoryDetailType1View].[SurveyPlanningId]

	WHERE		[ProjectCandidateCategoryDetailType1View].[ProjectCandidateId] = @ProjectCandidateId
END