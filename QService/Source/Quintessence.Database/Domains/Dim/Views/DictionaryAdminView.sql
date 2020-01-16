CREATE VIEW [dbo].[DictionaryAdminView] AS
	SELECT		[Dictionary].[Id],
				[Dictionary].[ContactId],
				[Dictionary].[Name],
				[Dictionary].[Current],
				[Dictionary].[Description],
				[Dictionary].[IsLive],
				[Dictionary].[Audit_CreatedBy],
				[Dictionary].[Audit_CreatedOn],
				[Dictionary].[Audit_ModifiedBy],
				[Dictionary].[Audit_ModifiedOn],
				[Dictionary].[Audit_DeletedBy],
				[Dictionary].[Audit_DeletedOn],
				[Dictionary].[Audit_IsDeleted],
				[Dictionary].[Audit_VersionId],
				[Dictionary].[LegacyId],
				(SELECT COUNT(*) FROM [AssessmentDevelopmentProjectView] WHERE [AssessmentDevelopmentProjectView].[DictionaryId] = [Dictionary].[Id]) AS [NumberOfUsages]

	FROM		[Dictionary]	WITH (NOLOCK)

	WHERE		[Dictionary].[Audit_IsDeleted] = 0