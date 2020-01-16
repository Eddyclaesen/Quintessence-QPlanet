--act projects
DECLARE @ACDCId AS UNIQUEIDENTIFIER
SELECT		@ACDCId = Id
FROM		[ProjectTypeView]
WHERE		[Code] = 'ACDC'

DECLARE @FDId AS UNIQUEIDENTIFIER
SELECT		@FDId = [Id]
FROM		[ProjectTypeCategoryView]
WHERE		[Code] = 'FD'

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
				@FDId,
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

	WHERE		ActProjectDetail.[projecttype] = 14	--FD
		AND		ActProject.[deleted] = 0
		AND		ActProjectDetail.[PostDate] IS NOT NULL

INSERT INTO [Project]([Id], [Name], [ProjectTypeId], [ContactId], [ProjectManagerId], [CustomerAssistantId], [PricingModelId], [StatusCode], [Remarks], [DepartmentInformation], [Audit_CreatedOn])
	SELECT		[Id], 
				[Name], 
				[ProjectTypeId], 
				[ContactId], 
				[ProjectManagerId], 
				[CustomerAssistantId], 
				1, --Time & Material 
				[StatusCode], 
				[Remarks], 
				[DepartmentInformation],
				[CreatedOn]
	FROM		[ProjectHistory]
	WHERE		[ProjectTypeCategoryId] = @FDId

INSERT INTO [AssessmentDevelopmentProject]([Id], [CandidateScoreReportTypeId], [FunctionTitle], [FunctionInformation], [DictionaryId])
	SELECT	[Id], 2, [FunctionTitle], [FunctionInformation], [DictionaryId]
	FROM	[ProjectHistory]
	WHERE	[ProjectHistory].[ProjectTypeId] = @ACDCId
	AND		[ProjectHistory].[ProjectTypeCategoryId] = @FDId

INSERT INTO	[ProjectCategoryDetail]([Id], [ProjectId], [UnitPrice], [ProjectTypeCategoryId])
	SELECT	NEWID(), [Id], 0, @FDId
	FROM	[ProjectHistory]
	WHERE	[ProjectHistory].[ProjectTypeId] = @ACDCId
	AND		[ProjectHistory].[ProjectTypeCategoryId] = @FDId

INSERT INTO [ProjectCategoryFdDetail]([Id], [ScoringTypeCode])
	SELECT	[Id], 20
	FROM	[ProjectCategoryDetail]
	WHERE	[ProjectCategoryDetail].[ProjectTypeCategoryId] = @FDId
GO

--COUPLE SIMULATIONS
INSERT INTO	[ProjectCategoryDetail2SimulationCombination]([Id], [ProjectCategoryDetailId], [SimulationCombinationId])
	SELECT		NEWID(), [ProjectCategoryFdDetail].[Id], [SimulationCombination].[Id]
	FROM		[ProjectCategoryFdDetail]
	INNER JOIN	[ProjectCategoryDetail]
		ON		[ProjectCategoryDetail].[Id] = [ProjectCategoryFdDetail].[Id]
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