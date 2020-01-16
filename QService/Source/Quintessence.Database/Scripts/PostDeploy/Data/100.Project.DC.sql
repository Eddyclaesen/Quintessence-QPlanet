--act projects
DECLARE @ACDCId AS UNIQUEIDENTIFIER
SELECT		@ACDCId = Id
FROM		[ProjectTypeView]
WHERE		[Code] = 'ACDC'

DECLARE @DCId AS UNIQUEIDENTIFIER
SELECT		@DCId = [Id]
FROM		[ProjectTypeCategoryView]
WHERE		[Code] = 'DC'

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
								[CreatedOn]
							)
		
	SELECT		NEWID(),
				ActProject.[Omschrijving],
				@ACDCId,
				@DCId,
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
				ActProjectDetail.[PostDate]
				
	FROM		[$(actserver)].[$(act)].[dbo].[projecten] ActProject

	INNER JOIN	[$(actserver)].[$(act)].[dbo].[projectfiche] ActProjectDetail
		ON		ActProjectDetail.[project_id] = ActProject.[project_id]

	INNER JOIN	[CrmContactView]
		ON		[CrmContactView].[Id] = ActProject.[Contact_id]

	INNER JOIN	[UserView] ProjectManagerUser
		ON		ProjectManagerUser.AssociateId = ActProjectDetail.Proma_id

	INNER JOIN	[UserView] CustomerAssistantUser
		ON		CustomerAssistantUser.AssociateId = [CrmContactView].[CustomerAssistantId]

	INNER JOIN	[DictionaryView]
		ON		(
					[DictionaryView].[LegacyId] = ActProjectDetail.[woordenboek_id]
					OR
					ActProjectDetail.[woordenboek_id] IS NULL
				)

	WHERE		ActProjectDetail.[projecttype] = 7	--DC
		AND		ActProject.[deleted] = 0
		AND		ActProjectDetail.[PostDate] IS NOT NULL

INSERT INTO [Project]([Id], [Name], [ProjectTypeId], [ContactId], [ProjectManagerId], [CustomerAssistantId], [PricingModelId], [StatusCode], [Remarks], [DepartmentInformation], [Audit_CreatedOn])
	SELECT		[Id], 
				[Name], 
				[ProjectTypeId], 
				[ContactId], 
				[ProjectManagerId], 
				[CustomerAssistantId],
				1,--Time & Material 
				[StatusCode], 
				[Remarks], 
				[DepartmentInformation],
				[CreatedOn]
	FROM		[ProjectHistory]
	WHERE		[ProjectTypeCategoryId] = @DCId

INSERT INTO [AssessmentDevelopmentProject]([Id], [CandidateScoreReportTypeId], [FunctionTitle], [FunctionInformation], [DictionaryId])
	SELECT	[Id], 2, [FunctionTitle], [FunctionInformation], [DictionaryId]
	FROM	[ProjectHistory]
	WHERE	[ProjectHistory].[ProjectTypeId] = @ACDCId
	AND		[ProjectHistory].[ProjectTypeCategoryId] = @DCId

INSERT INTO	[ProjectCategoryDetail]([Id], [ProjectId], [UnitPrice], [ProjectTypeCategoryId])
	SELECT	NEWID(), [Id], 0, @DCId
	FROM	[ProjectHistory]
	WHERE	[ProjectHistory].[ProjectTypeId] = @ACDCId
	AND		[ProjectHistory].[ProjectTypeCategoryId] = @DCId

INSERT INTO [ProjectCategoryDcDetail]([Id], [ScoringTypeCode])
	SELECT	[Id], 10
	FROM	[ProjectCategoryDetail]
	WHERE	[ProjectCategoryDetail].[ProjectTypeCategoryId] = @DCId
GO

--COUPLE SIMULATIONS
INSERT INTO	[ProjectCategoryDetail2SimulationCombination]([Id], [ProjectCategoryDetailId], [SimulationCombinationId])
	SELECT		NEWID(), [ProjectCategoryDcDetail].[Id], [SimulationCombination].[Id]
	FROM		[ProjectCategoryDcDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryDcDetail].[Id]
	INNER JOIN	[Project]
		ON		[Project].[Id] = [ProjectCategoryDetail].[ProjectId]
	INNER JOIN	[ProjectHistory]
		ON		[ProjectHistory].[Id] = [Project].[Id]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[ProjFiche_AcSim_Rel] ProjectFicheSimulationCombination
		ON		ProjectFicheSimulationCombination.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[ACSimulatieSet] ActSimulationCombination
		ON		ActSimulationCombination.[ACSimulatieSet_ID] = ProjectFicheSimulationCombination.[ACSimulatieSet_ID]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[Programma] ActSimulation
		ON		ActSimulation.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[SimulationSet]
		ON		[SimulationSet].[LegacyId] = ActSimulationCombination.[SimulatieSet_Id]
	INNER JOIN	[SimulationDepartment]
		ON		[SimulationDepartment].[LegacyId] = ActSimulationCombination.[Groep_id]
	INNER JOIN	[SimulationLevel]
		ON		[SimulationLevel].[LegacyId] = ActSimulationCombination.[Level_id]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[Select_Oef] SelectedExcersice
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
	SELECT		NEWID(), [ProjectCategoryDcDetail].[Id], [DictionaryIndicator].[Id], 0
	FROM		[ProjectCategoryDcDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryDcDetail].[Id]
	INNER JOIN	[Project]
		ON		[Project].[Id] = [ProjectCategoryDetail].[ProjectId]
	INNER JOIN	[ProjectHistory]
		ON		[ProjectHistory].[Id] = [Project].[Id]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[ProjFiche_CompProf] ActCompetence
		ON		ActCompetence.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[W_Samengesteld] ActDictionary
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
	SELECT		NEWID(), [ProjectCategoryDcDetail].[Id], [DictionaryCompetence].[Id], [SimulationCombination].[Id]
	FROM		[ProjectCategoryDcDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryDcDetail].[Id]
	INNER JOIN	[Project]
		ON		[Project].[Id] = [ProjectCategoryDetail].[ProjectId]
	INNER JOIN	[ProjectHistory]
		ON		[ProjectHistory].[Id] = [Project].[Id]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[Programma] ActProgram
		ON		ActProgram.[ACProject_ID] = [ProjectHistory].[ActId]
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[Programma_Competenties] ActMatrix
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
	INNER JOIN	[$(ACTSERVER)].[$(ACT)].[dbo].[select_oef] ActSelectedExersice
		ON		ActSelectedExersice.[select_oef_id] = ActProgram.[select_oef_id]
	INNER JOIN	[Simulation]
		ON		[Simulation].[LegacyId] = ActSelectedExersice.[oefening_id]
	INNER JOIN	[SimulationCombination]
		ON		[SimulationCombination].[SimulationId] = [Simulation].[Id]
	INNER JOIN	[ProjectCategoryDetail2SimulationCombination]
		ON		[ProjectCategoryDetail2SimulationCombination].[SimulationCombinationId] = [SimulationCombination].[Id]