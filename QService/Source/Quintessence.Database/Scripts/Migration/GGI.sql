BEGIN TRAN
INSERT INTO ProjectCategoryDetail2SimulationCombination
	SELECT	NEWID(),
			dbo.ProjectCategoryDetail.Id,
			'9B67B86F-5F50-420B-B286-D7FA16CF7649'
	  FROM actserver.Act.[dbo].[Programma_Gespr] Programma_Gespr
	INNER JOIN actserver.Act.[dbo].[Gespreksmoment] Gespreksmoment
		   ON Gespreksmoment.[Gespreksmoment_ID] = Programma_Gespr.[Gespreksmoment_ID]
	INNER JOIN actserver.Act.dbo.ProjectFiche ProjectFiche
		   ON ProjectFiche.ACProject_ID = Programma_Gespr.ACProject_ID
	INNER JOIN dbo.Project ON project.LegacyId = ProjectFiche.Project_ID
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN dbo.ProjectTypeCategory ON dbo.ProjectCategoryDetail.ProjectTypeCategoryId = dbo.ProjectTypeCategory.Id
	INNER JOIN dbo.ProjectType2ProjectTypeCategory ON dbo.ProjectTypeCategory.Id = dbo.ProjectType2ProjectTypeCategory.ProjectTypeCategoryId
			AND dbo.ProjectType2ProjectTypeCategory.IsMain = 1
	LEFT JOIN dbo.ProjectCategoryDetail2SimulationCombination 
			ON dbo.ProjectCategoryDetail2SimulationCombination.ProjectCategoryDetailId = dbo.ProjectCategoryDetail.Id
			AND dbo.ProjectCategoryDetail2SimulationCombination.SimulationCombinationId = '9B67B86F-5F50-420B-B286-D7FA16CF7649'
	WHERE [Gespreksmoment].Omschrijving = 'Gedragsgericht en gestructureerd interview'
	AND	dbo.ProjectCategoryDetail2SimulationCombination.SimulationCombinationId IS NULL
ROLLBACK


BEGIN TRAN
INSERT INTO dbo.ProjectCategoryDetail2Competence2Combination
	SELECT		DISTINCT
				NEWID(),
				dbo.ProjectCategoryDetail.Id,
				dbo.DictionaryCompetence.Id,
				dbo.ProjectCategoryDetail2SimulationCombination.SimulationCombinationId
				
	FROM		actserver.[ACT].[dbo].[Programma_Gespr] [Programma_Gespr]
	INNER JOIN	actserver.[ACT].[dbo].[Gespr_Comps] [Gespr_Comps]
		ON		[Gespr_Comps].[Programma_Gespr_ID] = [Programma_Gespr].[Programma_Gespr_ID]
	INNER JOIN	actserver.[ACT].[dbo].[ProjectFiche] [ProjectFiche]
		ON		[ProjectFiche].[ACProject_ID] = [Programma_Gespr].[ACProject_ID]
	INNER JOIN	[dbo].[Project]
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]
	INNER JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = project.Id
	INNER JOIN	dbo.ProjectCategoryDetail2SimulationCombination 
		ON		dbo.ProjectCategoryDetail2SimulationCombination.ProjectCategoryDetailId = dbo.ProjectCategoryDetail.Id
		AND		dbo.ProjectCategoryDetail2SimulationCombination.SimulationCombinationId = '9B67B86F-5F50-420B-B286-D7FA16CF7649'
	INNER JOIN	dbo.AssessmentDevelopmentProject
		ON		dbo.AssessmentDevelopmentProject.Id = dbo.Project.Id
	INNER JOIN	dbo.Dictionary
		ON		dbo.Dictionary.Id = dbo.AssessmentDevelopmentProject.DictionaryId
	INNER JOIN	dbo.DictionaryCluster
		ON		dbo.DictionaryCluster.DictionaryId = dbo.Dictionary.Id
	INNER JOIN	dbo.DictionaryCompetence
		ON		dbo.DictionaryCompetence.DictionaryClusterId = dbo.DictionaryCluster.Id
		AND		dbo.DictionaryCompetence.LegacyId = [Gespr_Comps].[Competentie_ID]
	LEFT JOIN	dbo.ProjectCategoryDetail2Competence2Combination
		ON		dbo.ProjectCategoryDetail2Competence2Combination.DictionaryCompetenceId = dbo.DictionaryCompetence.Id
		AND		dbo.ProjectCategoryDetail2Competence2Combination.SimulationCombinationId = dbo.ProjectCategoryDetail2SimulationCombination.SimulationCombinationId
	WHERE		dbo.ProjectCategoryDetail2Competence2Combination.SimulationCombinationId IS NULL
ROLLBACK