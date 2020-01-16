DECLARE @ACDCId AS UNIQUEIDENTIFIER
SELECT		@ACDCId = Id
FROM		[ProjectTypeView]
WHERE		[Code] = 'ACDC'

DECLARE @ACId AS UNIQUEIDENTIFIER
SELECT		@ACId = [Id]
FROM		[ProjectTypeCategoryView]
WHERE		[Code] = 'AC'

INSERT INTO	ProjectHistory(
								[Id],
								[Name],
								[ProjectTypeId],
								[ProjectTypeCategoryId],
								[ContactId],
								[ProjectManagerId],
								[CustomerAssistantId],
								[StatusCode],
								[Remarks],
								[FunctionTitle],
								[FunctionInformation],
								[DepartmentInformation],
								[CrmProjectId],
								[DictionaryId],
								[ActId],
								[CreatedOn])
		
	SELECT		NEWID(),
				ActProject.[Omschrijving],
				@ACDCId,
				@ACId,
				ActProject.[Contact_id],
				ProjectManagerUser.[Id],
				CustomerAssistantUser.[Id],
				10,
				ActProjectDetail.[OpmVerslag],
				ActProjectDetail.[FunctieTitel],
				ActProjectDetail.[FunctieInfo],
				NULL,
				ActProjectDetail.[SOProject_Id],
				[DictionaryView].[Id],
				ActProjectDetail.[ACProject_ID],
				ISNULL(ActProjectDetail.[PostDate], '2001-01-01')
				
	FROM		[$(ActServer)].[$(Act)].[dbo].[projecten] ActProject
	
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[ProjectToMigrate] [ProjectToMigrate]
		ON		[ProjectToMigrate].[Id] = ActProject.[project_id]
		AND		[ProjectToMigrate].[ProjectTypeCode] = 'AC'

	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[projectfiche] ActProjectDetail
		ON		ActProjectDetail.[acproject_id] = [ProjectToMigrate].[ProjectFicheId]

	LEFT JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = ActProject.[Contact_id]

	LEFT JOIN	[UserView] ProjectManagerUser
		ON		ProjectManagerUser.AssociateId = ActProjectDetail.Proma_id

	LEFT JOIN	[UserView] CustomerAssistantUser
		ON		CustomerAssistantUser.AssociateId = [CrmContactView].[CustomerAssistantId]

	LEFT JOIN	[DictionaryView]
		ON		(
					[DictionaryView].[LegacyId] = ActProjectDetail.[woordenboek_id]
					OR
					ActProjectDetail.[woordenboek_id] IS NULL
				)

	WHERE		ActProject.[deleted] = 0

INSERT INTO [Project]([Id], [Name], [ProjectTypeId], [ContactId], [ProjectManagerId], [CustomerAssistantId], [StatusCode], [Remarks], [DepartmentInformation], [PricingModelId], [Audit_CreatedOn])
	SELECT		[Id], 
				[Name], 
				[ProjectTypeId], 
				[ContactId], 
				[ProjectManagerId], 
				[CustomerAssistantId], 
				[StatusCode], 
				[Remarks], 
				[DepartmentInformation],
				1,
				[CreatedOn]
	FROM		[ProjectHistory]
	WHERE		[ProjectTypeCategoryId] = @ACId

INSERT INTO [AssessmentDevelopmentProject]([Id], [CandidateScoreReportTypeId], [FunctionTitle], [FunctionInformation], [DictionaryId])
	SELECT	[Id], 2, [FunctionTitle], [FunctionInformation], [ProjectHistory].[DictionaryId]
	FROM	[ProjectHistory]
	WHERE	[ProjectHistory].[ProjectTypeId] = @ACDCId
	AND		[ProjectHistory].[ProjectTypeCategoryId] = @ACId

INSERT INTO	[ProjectCategoryDetail]([Id], [ProjectId], [UnitPrice], [ProjectTypeCategoryId])
	SELECT	NEWID(), [Id], 0, @ACId
	FROM	[ProjectHistory]
	WHERE	[ProjectHistory].[ProjectTypeId] = @ACDCId
	AND		[ProjectHistory].[ProjectTypeCategoryId] = @ACId

INSERT INTO [ProjectCategoryAcDetail]([Id], [ScoringTypeCode])
	SELECT	[Id], 10
	FROM	[ProjectCategoryDetail]
	WHERE	[ProjectCategoryDetail].[ProjectTypeCategoryId] = @ACId
GO

--COUPLE SIMULATIONS
INSERT INTO	[ProjectCategoryDetail2SimulationCombination]([Id], [ProjectCategoryDetailId], [SimulationCombinationId])
	SELECT		NEWID(), [ProjectCategoryAcDetail].[Id], [SimulationCombination].[Id]
	FROM		[ProjectCategoryAcDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryAcDetail].[Id]
	INNER JOIN	[Project]
		ON		[Project].[Id] = [ProjectCategoryDetail].[ProjectId]
	INNER JOIN	[ProjectHistory]
		ON		[ProjectHistory].[Id] = [Project].[Id]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[ProjFiche_AcSim_Rel] ProjectFicheSimulationCombination
		ON		ProjectFicheSimulationCombination.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[ACSimulatieSet] ActSimulationCombination
		ON		ActSimulationCombination.[ACSimulatieSet_ID] = ProjectFicheSimulationCombination.[ACSimulatieSet_ID]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[Programma] ActSimulation
		ON		ActSimulation.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[SimulationSet]
		ON		[SimulationSet].[LegacyId] = ActSimulationCombination.[SimulatieSet_Id]
	INNER JOIN	[SimulationDepartment]
		ON		[SimulationDepartment].[LegacyId] = ActSimulationCombination.[Groep_id]
	INNER JOIN	[SimulationLevel]
		ON		[SimulationLevel].[LegacyId] = ActSimulationCombination.[Level_id]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[Select_Oef] SelectedExcersice
		ON		SelectedExcersice.[Select_oef_id] = ActSimulation.[Select_oef_id]
	INNER JOIN	[Simulation]
		ON		[Simulation].[LegacyId] = SelectedExcersice.[oefening_id]
	INNER JOIN	[SimulationCombination]
		ON		[SimulationCombination].[SimulationSetId] = [SimulationSet].[Id]
			AND	[SimulationCombination].[SimulationDepartmentId] = [SimulationDepartment].[Id]
			AND	[SimulationCombination].[SimulationLevelId] = [SimulationLevel].[Id]
			AND	[SimulationCombination].[SimulationId] = [Simulation].[Id]

--COUPLE INDICATORS
INSERT INTO [ProjectCategoryDetail2DictionaryIndicator]([Id], [ProjectCategoryDetailId], [DictionaryIndicatorId], [IsDefinedByRole])
	SELECT		NEWID(), [ProjectCategoryAcDetail].[Id], [DictionaryIndicator].[Id], 0
	FROM		[ProjectCategoryAcDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryAcDetail].[Id]
	INNER JOIN	[Project]
		ON		[Project].[Id] = [ProjectCategoryDetail].[ProjectId]
	INNER JOIN	[ProjectHistory]
		ON		[ProjectHistory].[Id] = [Project].[Id]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[ProjFiche_CompProf] ActCompetence
		ON		ActCompetence.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[W_Samengesteld] ActDictionary
		ON		ActDictionary.[W_Samengesteld_ID] = ActCompetence.[W_Samengesteld_ID]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[LegacyId] = ActDictionary.[Indicator_id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[Id] = [DictionaryIndicator].[DictionaryLevelId]
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[Id] = [DictionaryLevel].[DictionaryCompetenceId]
		AND		[DictionaryCompetence].[LegacyId] = ActDictionary.[competentie_id]
	INNER JOIN	[DictionaryCluster]
		ON		[DictionaryCluster].[Id] = [DictionaryCompetence].[DictionaryClusterId]
		AND		[DictionaryCluster].[LegacyId] = ActDictionary.[cluster_id]
	INNER JOIN	[Dictionary]
		ON		[Dictionary].[Id] = [DictionaryCluster].[DictionaryId]
		AND		[Dictionary].[LegacyId] = ActDictionary.[woordenboek_id]

--Matrix
INSERT INTO	[ProjectCategoryDetail2Competence2Combination]([Id], [ProjectCategoryDetailId], [DictionaryCompetenceId], [SimulationCombinationId])
	SELECT		NEWID(), [ProjectCategoryAcDetail].[Id], [DictionaryCompetence].[Id], [SimulationCombination].[Id]
	FROM		[ProjectCategoryAcDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryAcDetail].[Id]
	INNER JOIN	[Project]
		ON		[Project].[Id] = [ProjectCategoryDetail].[ProjectId]
	INNER JOIN	[ProjectHistory]
		ON		[ProjectHistory].[Id] = [Project].[Id]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[Programma] ActProgram
		ON		ActProgram.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[Programma_Competenties] ActMatrix
		ON		ActMatrix.[programma_id] = ActProgram.[Programma_id]

	--Competence
	INNER JOIN	[DictionaryCompetence]
		ON		[DictionaryCompetence].[LegacyId] = ActMatrix.[Competentie_id]
	INNER JOIN	[DictionaryLevel]
		ON		[DictionaryLevel].[DictionaryCompetenceId] = [DictionaryCompetence].[Id]
	INNER JOIN	[DictionaryIndicator]
		ON		[DictionaryIndicator].[DictionaryLevelId] = [DictionaryLevel].[Id]
	INNER JOIN	[ProjectCategoryDetail2DictionaryIndicator]
		ON		[ProjectCategoryDetail2DictionaryIndicator].[DictionaryIndicatorId] = [DictionaryIndicator].[Id]
			AND	[ProjectCategoryDetail2DictionaryIndicator].[ProjectCategoryDetailId] = [ProjectCategoryDetail].[Id]

	--Simulation
	INNER JOIN	[$(ActServer)].[$(Act)].[dbo].[select_oef] ActSelectedExersice
		ON		ActSelectedExersice.[select_oef_id] = ActProgram.[select_oef_id]
	INNER JOIN	[Simulation]
		ON		[Simulation].[LegacyId] = ActSelectedExersice.[oefening_id]
	INNER JOIN	[SimulationCombination]
		ON		[SimulationCombination].[SimulationId] = [Simulation].[Id]
	INNER JOIN	[ProjectCategoryDetail2SimulationCombination]
		ON		[ProjectCategoryDetail2SimulationCombination].[SimulationCombinationId] = [SimulationCombination].[Id]


--COUPLE CANDIDATES
