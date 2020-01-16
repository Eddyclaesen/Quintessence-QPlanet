CREATE VIEW [dbo].[ProjectCandidateResumeFieldView] AS
	SELECT		[ProjectCandidateResumeField].*,
				[CandidateReportDefinitionFieldView].[Name]		AS	[CandidateReportDefinitionFieldName]
				
	FROM		[ProjectCandidateResumeField]	WITH (NOLOCK)

	INNER JOIN	[CandidateReportDefinitionFieldView]
		ON		[CandidateReportDefinitionFieldView].[Id] = [ProjectCandidateResumeField].[CandidateReportDefinitionFieldId]

	WHERE		[ProjectCandidateResumeField].[Audit_IsDeleted] = 0