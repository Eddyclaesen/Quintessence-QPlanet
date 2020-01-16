DELETE FROM ProjectCategoryDetail2SimulationCombination

DECLARE @SimulationTable AS TABLE(Simulation1Id INT, Simulation2Id INT, Simulation3Id INT, SimulationLevelId INT, SimulationDepartmentId INT, SimulationSetId INT)

INSERT INTO @SimulationTable
	SELECT dbo.Simulation.Legacy1Id, dbo.Simulation.Legacy2Id, dbo.Simulation.Legacy3Id, dbo.SimulationLevel.LegacyId, dbo.SimulationDepartment.LegacyId, dbo.SimulationSet.LegacyId FROM dbo.SimulationCombination
	LEFT JOIN dbo.SimulationSet ON dbo.SimulationCombination.SimulationSetId = dbo.SimulationSet.Id
	LEFT JOIN dbo.SimulationDepartment ON dbo.SimulationCombination.SimulationDepartmentId = dbo.SimulationDepartment.Id
	LEFT JOIN dbo.SimulationLevel ON dbo.SimulationCombination.SimulationLevelId = dbo.SimulationLevel.Id
	LEFT JOIN dbo.Simulation ON dbo.SimulationCombination.SimulationId = dbo.Simulation.Id

DECLARE @MigrationTable AS TABLE(ProjectId INT, SimulationSetId INT, SimulationDepartmentId INT, SimulationLevelId INT, SimulationId INT)
INSERT INTO @MigrationTable
	SELECT DISTINCT ProjectToMigrate.ProjectFicheId, ActSimulationSet.SimulatieSet_ID, ActDepartment.Groep_ID, ActLevel.Level_ID, ActOefening.Oefening_ID
	FROM actserver.ACT.dbo.ProjectToMigrate ProjectToMigrate
	INNER JOIN actserver.ACT.dbo.ProjFiche_AcSim_Rel ProjectSimulation ON ProjectSimulation.ACProject_ID = ProjectToMigrate.ProjectFicheId
	INNER JOIN ACTSERVER.ACT.dbo.ACSimulatieSet SimulationCombination ON SimulationCombination.ACSimulatieSet_ID = ProjectSimulation.ACSimulatieSet_ID
	INNER JOIN ACTSERVER.ACT.dbo.Programma Program ON Program.ACProject_ID = ProjectToMigrate.ProjectFicheId
	INNER JOIN ACTSERVER.ACT.dbo.Select_Oef SelectedExcersice ON SelectedExcersice.Select_Oef_ID = Program.Select_Oef_ID
	LEFT JOIN ACTSERVER.ACT.dbo.SimulatieSet ActSimulationSet ON ActSimulationSet.SimulatieSet_ID = SimulationCombination.SimulatieSet_ID
	LEFT JOIN ACTSERVER.ACT.dbo.Groep ActDepartment ON ActDepartment.Groep_ID = SimulationCombination.Groep_ID AND ActDepartment.Omschrijving <> 'geen'
	LEFT JOIN ACTSERVER.ACT.dbo.Level ActLevel ON ActLevel.Level_ID = SimulationCombination.Level_ID AND ActLevel.Omschrijving <> 'geen'
	LEFT JOIN ACTSERVER.ACT.dbo.Oefening ActOefening ON ActOefening.Oefening_ID = SelectedExcersice.Oefening_ID
	LEFT JOIN @SimulationTable SimulationTable
		ON		(
					SimulationTable.Simulation1Id = SelectedExcersice.Oefening_ID
				OR
					SimulationTable.Simulation2Id = SelectedExcersice.Oefening_ID
				OR
					SimulationTable.Simulation3Id = SelectedExcersice.Oefening_ID
				)
		AND		(
					(SimulationTable.SimulationDepartmentId = SimulationCombination.Groep_ID)
				OR
					(SimulationTable.SimulationDepartmentId IS NULL AND SimulationCombination.Groep_ID IS NULL)
				)
		
		AND		(
					(SimulationTable.SimulationLevelId = SimulationCombination.Level_ID)
				OR
					(SimulationTable.SimulationLevelId IS NULL AND SimulationCombination.Level_ID IS NULL)
				)
		AND		SimulationTable.SimulationSetId = SimulationCombination.SimulatieSet_ID

DECLARE @QPlanetMigrationTable AS TABLE(ProjectId UNIQUEIDENTIFIER, SimulationSetId UNIQUEIDENTIFIER, SimulationDepartmentId UNIQUEIDENTIFIER, SimulationLevelId UNIQUEIDENTIFIER, SimulationId UNIQUEIDENTIFIER)
INSERT INTO @QPlanetMigrationTable
	SELECT DISTINCT dbo.Project.Id, dbo.SimulationSet.Id, dbo.SimulationDepartment.Id, dbo.SimulationLevel.Id, dbo.Simulation.Id FROM @MigrationTable MigrationTable
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche ON ProjectFiche.ACProject_ID = MigrationTable.ProjectId
	INNER JOIN dbo.Project ON dbo.Project.LegacyId = ProjectFiche.Project_ID
	INNER JOIN dbo.SimulationSet ON dbo.SimulationSet.LegacyId = MigrationTable.SimulationSetId
	LEFT JOIN dbo.SimulationDepartment ON dbo.SimulationDepartment.LegacyId = MigrationTable.SimulationDepartmentId
	LEFT JOIN dbo.SimulationLevel ON dbo.SimulationLevel.LegacyId = MigrationTable.SimulationLevelId
	INNER JOIN dbo.Simulation ON dbo.Simulation.Legacy1Id = MigrationTable.SimulationId OR dbo.Simulation.Legacy2Id = MigrationTable.SimulationId OR dbo.Simulation.Legacy3Id = MigrationTable.SimulationId

INSERT INTO dbo.ProjectCategoryDetail2SimulationCombination(Id, ProjectCategoryDetailId, SimulationCombinationId)
	SELECT NEWID(), dbo.ProjectCategoryDetail.Id, dbo.SimulationCombination.Id FROM @QPlanetMigrationTable QPlanetMigrationTable
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryDetail.ProjectId = QPlanetMigrationTable.ProjectId
	LEFT JOIN dbo.SimulationCombination
		ON dbo.SimulationCombination.SimulationSetId = QPlanetMigrationTable.SimulationSetId
		AND COALESCE(dbo.SimulationCombination.SimulationDepartmentId, 'ADBF5EAF-658B-426C-BFFF-8D58964FD31F') = COALESCE(QPlanetMigrationTable.SimulationDepartmentId, 'ADBF5EAF-658B-426C-BFFF-8D58964FD31F')
		AND COALESCE(dbo.SimulationCombination.SimulationLevelId, 'ADBF5EAF-658B-426C-BFFF-8D58964FD31F') = COALESCE(QPlanetMigrationTable.SimulationLevelId, 'ADBF5EAF-658B-426C-BFFF-8D58964FD31F')
		AND dbo.SimulationCombination.SimulationId = QPlanetMigrationTable.SimulationId
	WHERE dbo.SimulationCombination.Id IS NOT NULL