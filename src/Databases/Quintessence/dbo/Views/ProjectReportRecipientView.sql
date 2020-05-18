CREATE VIEW [dbo].[ProjectReportRecipientView]
	AS 
	SELECT		[ProjectReportRecipient].*,
				[CrmEmailView].[FirstName],
				[CrmEmailView].[LastName],
				[CrmEmailView].[Email],
				[CrmEmailView].[DirectPhone],
				[CrmEmailView].[MobilePhone]

	FROM		[ProjectReportRecipient] WITH (NOLOCK)

	INNER JOIN	[CrmEmailView] 
		ON		[ProjectReportRecipient].CrmEmailId = [CrmEmailView].Id 

	WHERE		[ProjectReportRecipient].[Audit_IsDeleted] = 0