CREATE VIEW [dbo].[AssessmentDevelopmentProjectView] AS
	SELECT		[AssessmentDevelopmentProject].[Id],
				[AssessmentDevelopmentProject].[FunctionTitle],
				[AssessmentDevelopmentProject].[FunctionInformation],
				[AssessmentDevelopmentProject].[DictionaryId],
				[AssessmentDevelopmentProject].[CandidateReportDefinitionId],
				[AssessmentDevelopmentProject].[CandidateScoreReportTypeId],
				[AssessmentDevelopmentProject].[ReportDeadlineStep],
				[AssessmentDevelopmentProject].[PhoneCallRemarks],
				[AssessmentDevelopmentProject].[ReportRemarks],
				[AssessmentDevelopmentProject].[IsRevisionByPmRequired],
				[AssessmentDevelopmentProject].[SendReportToParticipant],
				[AssessmentDevelopmentProject].[SendReportToParticipantRemarks],
				[ProjectTypeCategoryView].[Code]									AS	[ProjectTypeCategoryCode]

	FROM		[AssessmentDevelopmentProject]	WITH (NOLOCK)

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [AssessmentDevelopmentProject].[Id]

	LEFT JOIN	[ProjectCategoryDetailView] 
		ON		[ProjectCategoryDetailView].[ProjectId] = [ProjectView].[Id]

	LEFT JOIN	[ProjectTypeCategoryView]
		ON		[ProjectTypeCategoryView].[Id] = [ProjectCategoryDetailView].[ProjectTypeCategoryId]

	WHERE		[ProjectTypeCategoryView].[Id] IS NULL OR [ProjectTypeCategoryView].[IsMain] = 1