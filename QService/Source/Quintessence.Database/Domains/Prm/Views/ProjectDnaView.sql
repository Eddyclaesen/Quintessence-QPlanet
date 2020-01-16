CREATE VIEW [dbo].[ProjectDnaView] AS
	SELECT		[ProjectDna].*,
				[CrmProjectView].[name]				AS	[CrmProjectName],
				[CrmProjectView].[ContactId]		AS	[ContactId],
				[CrmContactView].[Name]				AS	[ContactName],
				[CrmContactView].[Department]		AS	[ContactDepartment]

	FROM		[ProjectDna]	WITH (NOLOCK)

	INNER JOIN	[CrmProjectView]
		ON		[CrmProjectView].[Id] = [ProjectDna].[CrmProjectId]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = [CrmProjectView].[ContactId]

	WHERE		[ProjectDna].[Audit_IsDeleted] = 0
