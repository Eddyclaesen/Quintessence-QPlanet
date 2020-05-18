CREATE PROCEDURE Reporting_ProjectDetailCandidateCategoryDetailTypes3
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeName]		AS	[ProjectCategoryDetailTypeName],
				[ProjectCandidateCategoryDetailType3View].[Deadline]							AS	[ScheduledDate],
				[ProjectCandidateCategoryDetailType3View].[LoginCode]							AS	[LoginCode],
				[ProjectCategoryDetailType3View].[SurveyPlanningId]								AS	[SurveyPlanningId],
				[SurveyPlanning].[Text]															AS	[SurveyPlanningText]

	FROM		[ProjectCandidateCategoryDetailType3View]

	INNER JOIN	[ProjectCategoryDetailType3View]
		ON		[ProjectCategoryDetailType3View].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCategoryDetailTypeId]

	INNER JOIN	[SurveyPlanning]
		ON		[SurveyPlanning].[Id] = [ProjectCategoryDetailType3View].[SurveyPlanningId]

	WHERE		[ProjectCandidateCategoryDetailType3View].[ProjectCandidateId] = @ProjectCandidateId
END