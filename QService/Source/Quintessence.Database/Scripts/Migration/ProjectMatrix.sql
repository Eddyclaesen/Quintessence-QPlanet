DELETE FROM [ProjectCategoryDetail2Competence2Combination]

DECLARE @CompetenceCombinationTable AS TABLE(ProjectCategoryDetailId UNIQUEIDENTIFIER, DictionaryCompetenceId INT, SimulationId INT)
INSERT INTO @CompetenceCombinationTable
	SELECT DISTINCT dbo.ProjectCategoryDetail.Id, ActMatrix.Competentie_ID, ActSimulation.Oefening_ID FROM dbo.ProjectCategoryDetail
	INNER JOIN dbo.Project ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ActProjectFiche ON ActProjectFiche.Project_ID = dbo.Project.LegacyId
	INNER JOIN ACTSERVER.ACT.dbo.Programma ActProgram ON ActProgram.ACProject_ID = ActProjectFiche.ACProject_ID
	INNER JOIN ACTSERVER.ACT.dbo.Select_Oef ActSimulation ON ActSimulation.Select_Oef_ID = ActProgram.Select_Oef_ID
	INNER JOIN ACTSERVER.ACT.dbo.Programma_Competenties ActMatrix ON ActMatrix.Programma_ID = ActProgram.Programma_ID
	
DECLARE @CombinationImportTable AS TABLE(ProjectCategoryDetailId UNIQUEIDENTIFIER, DictionaryCompetenceId UNIQUEIDENTIFIER, SimulationCombinationId UNIQUEIDENTIFIER)
INSERT INTO @CombinationImportTable
	SELECT DISTINCT CompetenceCombinationTable.ProjectCategoryDetailId, dbo.DictionaryCompetence.Id, dbo.SimulationCombination.Id
	FROM @CompetenceCombinationTable CompetenceCombinationTable

	INNER JOIN dbo.DictionaryCompetence 
		ON dbo.DictionaryCompetence.LegacyId = CompetenceCombinationTable.DictionaryCompetenceId
	INNER JOIN dbo.DictionaryLevel 
		ON dbo.DictionaryLevel.DictionaryCompetenceId = dbo.DictionaryCompetence.Id
	INNER JOIN dbo.DictionaryIndicator 
		ON dbo.DictionaryIndicator.DictionaryLevelId = dbo.DictionaryLevel.Id
	INNER JOIN dbo.ProjectCategoryDetail2DictionaryIndicator 
		ON dbo.ProjectCategoryDetail2DictionaryIndicator.DictionaryIndicatorId = dbo.DictionaryIndicator.Id
		AND dbo.ProjectCategoryDetail2DictionaryIndicator.ProjectCategoryDetailId = CompetenceCombinationTable.ProjectCategoryDetailId

	INNER JOIN dbo.Simulation
		ON (dbo.Simulation.Legacy1Id IS NOT NULL AND dbo.Simulation.Legacy1Id = CompetenceCombinationTable.SimulationId)
		OR (dbo.Simulation.Legacy2Id IS NOT NULL AND dbo.Simulation.Legacy2Id = CompetenceCombinationTable.SimulationId)
		OR (dbo.Simulation.Legacy3Id IS NOT NULL AND dbo.Simulation.Legacy3Id = CompetenceCombinationTable.SimulationId)
	INNER JOIN dbo.SimulationCombination 
		ON dbo.SimulationCombination.SimulationId = dbo.Simulation.Id
	INNER JOIN dbo.ProjectCategoryDetail2SimulationCombination
		ON dbo.ProjectCategoryDetail2SimulationCombination.SimulationCombinationId = dbo.SimulationCombination.Id
		AND dbo.ProjectCategoryDetail2SimulationCombination.ProjectCategoryDetailId = CompetenceCombinationTable.ProjectCategoryDetailId

INSERT INTO [ProjectCategoryDetail2Competence2Combination]
	SELECT NEWID(), *
	FROM @CombinationImportTable