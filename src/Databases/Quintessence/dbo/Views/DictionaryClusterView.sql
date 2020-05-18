CREATE VIEW [DictionaryClusterView] AS
	SELECT		[Id],
				[DictionaryId],
				[Name],
				[Description],
				[Color],
				[Order],
				[ImageName],
				[Audit_CreatedBy],
				[Audit_CreatedOn],
				[Audit_ModifiedBy],
				[Audit_ModifiedOn],
				[Audit_DeletedBy],
				[Audit_DeletedOn],
				[Audit_IsDeleted],
				[Audit_VersionId],
				[LegacyId]	

	FROM		[DictionaryCluster]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0