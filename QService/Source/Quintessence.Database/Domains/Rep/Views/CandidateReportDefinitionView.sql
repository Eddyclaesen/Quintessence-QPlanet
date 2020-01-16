CREATE VIEW [dbo].[CandidateReportDefinitionView] AS
	SELECT		[CandidateReportDefinition].*,
				[CrmContactView].[name]				AS	ContactName,
				[CrmContactView].[department]		AS	ContactDepartment

	FROM		[CandidateReportDefinition]	WITH (NOLOCK)

	LEFT JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [CandidateReportDefinition].[ContactId]

	WHERE		[Audit_IsDeleted] = 0
