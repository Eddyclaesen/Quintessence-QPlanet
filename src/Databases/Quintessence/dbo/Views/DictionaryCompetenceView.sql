CREATE VIEW [dbo].[DictionaryCompetenceView] AS
	SELECT		[Id],
				[DictionaryClusterId],
				[Name],
				[Description],
				[Order],
				[Audit_CreatedBy],
				[Audit_CreatedOn],
				[Audit_ModifiedBy],
				[Audit_ModifiedOn],
				[Audit_DeletedBy],
				[Audit_DeletedOn],
				[Audit_IsDeleted],
				[Audit_VersionId],
				[LegacyId]	

	FROM		[DictionaryCompetence]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0