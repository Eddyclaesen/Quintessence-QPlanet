CREATE VIEW [dbo].[ProposalView] AS
	SELECT		[Proposal].[Id]						AS	[Id],
				[Proposal].[Name]					AS	[Name],
				[Proposal].[Description]			AS	[Description],
				[Proposal].[ContactId]				AS	[ContactId],
				[Proposal].[BusinessDeveloperId]	AS	[BusinessDeveloperId],
				[Proposal].[ExecutorId]				AS	[ExecutorId],
				[Proposal].[DateReceived]			AS	[DateReceived],
				[Proposal].[Deadline]				AS	[Deadline],
				[Proposal].[DateSent]				AS	[DateSent],
				[Proposal].[DateWon]				AS	[DateWon],
				[Proposal].[PriceEstimation]		AS	[PriceEstimation],
				[Proposal].[Prognosis]				AS	[Prognosis],
				[Proposal].[FinalBudget]			AS	[FinalBudget],
				[Proposal].[StatusCode]				AS	[StatusCode],
				[Proposal].[StatusReason]			AS	[StatusReason],
				[Proposal].[Audit_CreatedBy]		AS	[Audit_CreatedBy],
				[Proposal].[Audit_CreatedOn]		AS	[Audit_CreatedOn],
				[Proposal].[Audit_ModifiedBy]		AS	[Audit_ModifiedBy],
				[Proposal].[Audit_ModifiedOn]		AS	[Audit_ModifiedOn],
				[Proposal].[Audit_DeletedBy]		AS	[Audit_DeletedBy],
				[Proposal].[Audit_DeletedOn]		AS	[Audit_DeletedOn],
				[Proposal].[Audit_IsDeleted]		AS	[Audit_IsDeleted],
				[Proposal].[Audit_VersionId]		AS	[Audit_VersionId],
				[CrmContactView].[name]				AS	[ContactName],
				[CrmContactView].[department]		AS	[ContactDepartment],
				BusinessDeveloperUser.[FirstName]	AS	[BusinessDeveloperFirstName],
				BusinessDeveloperUser.[LastName]	AS	[BusinessDeveloperLastName],
				ExecutorUser.[FirstName]			AS	[ExecutorFirstName],
				ExecutorUser.[LastName]				AS	[ExecutorLastName]
				
	FROM		[Proposal]	WITH (NOLOCK)

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [Proposal].[ContactId]

	LEFT JOIN	[UserView]	BusinessDeveloperUser
		ON		BusinessDeveloperUser.[Id] = [Proposal].[BusinessDeveloperId]

	LEFT JOIN	[UserView] ExecutorUser
		ON		ExecutorUser.[Id] = [Proposal].[ExecutorId]
	
	WHERE		[Proposal].[Audit_IsDeleted] = 0