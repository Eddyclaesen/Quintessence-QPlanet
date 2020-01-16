CREATE VIEW [SimulationTranslationView] AS
	SELECT		[SimulationTranslation].[Id],
				[SimulationTranslation].[SimulationId],
				[SimulationTranslation].[LanguageId],
				[LanguageView].[Name]							AS	[LanguageName],
				[SimulationTranslation].[Name],
				[SimulationView].[Name]							AS	[SimulationName],
				[SimulationTranslation].[Audit_CreatedBy],
				[SimulationTranslation].[Audit_CreatedOn],
				[SimulationTranslation].[Audit_ModifiedBy],
				[SimulationTranslation].[Audit_ModifiedOn],
				[SimulationTranslation].[Audit_DeletedBy],
				[SimulationTranslation].[Audit_DeletedOn],
				[SimulationTranslation].[Audit_IsDeleted],
				[SimulationTranslation].[Audit_VersionId]
					
	FROM		[SimulationTranslation]	WITH (NOLOCK)

	INNER JOIN	[LanguageView]
		ON		[LanguageView].[Id] = [SimulationTranslation].[LanguageId]

	INNER JOIN	[SimulationView]
		ON		[SimulationView].[Id] = [SimulationTranslation].[SimulationId]

	WHERE		[SimulationTranslation].[Audit_IsDeleted] = 0