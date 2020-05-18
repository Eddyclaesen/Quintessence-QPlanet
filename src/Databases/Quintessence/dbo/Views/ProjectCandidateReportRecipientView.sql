CREATE VIEW [dbo].[ProjectCandidateReportRecipientView]
	AS 
	SELECT		[ProjectCandidateReportRecipient].*,
				[CrmEmailView].[FirstName],
				[CrmEmailView].[LastName],
				[CrmEmailView].[Email],
				[CrmEmailView].[DirectPhone],
				[CrmEmailView].[MobilePhone]

	FROM		[ProjectCandidateReportRecipient] WITH (NOLOCK)

	INNER JOIN	[CrmEmailView] 
		ON		[ProjectCandidateReportRecipient].CrmEmailId = [CrmEmailView].Id 

	WHERE		[ProjectCandidateReportRecipient].[Audit_IsDeleted] = 0