CREATE VIEW [dbo].[DictionaryIndicatorView] AS
	SELECT		[Id],
				[DictionaryLevelId],
				[Name],
				[Order],
				[IsStandard],
				[IsDistinctive],
				[Audit_CreatedBy],
				[Audit_CreatedOn],
				[Audit_ModifiedBy],
				[Audit_ModifiedOn],
				[Audit_DeletedBy],
				[Audit_DeletedOn],
				[Audit_IsDeleted],
				[Audit_VersionId],
				[LegacyId]

	FROM		[DictionaryIndicator]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0