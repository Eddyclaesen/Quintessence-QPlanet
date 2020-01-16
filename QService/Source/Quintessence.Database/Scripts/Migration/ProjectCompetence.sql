DELETE FROM dbo.ProjectCategoryDetail2DictionaryIndicator

--Assessment Center
INSERT INTO ProjectCategoryDetail2DictionaryIndicator(Id, ProjectCategoryDetailId, DictionaryIndicatorId, IsDefinedByRole, IsStandard, IsDistinctive)	
	SELECT	NEWID(),
			dbo.ProjectCategoryDetail.Id,
			dbo.DictionaryIndicator.Id,
			0,
			dbo.DictionaryIndicator.IsStandard,
			dbo.DictionaryIndicator.IsDistinctive
			
	FROM dbo.ProjectCategoryAcDetail
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryAcDetail.Id = dbo.ProjectCategoryDetail.Id
	INNER JOIN dbo.Project ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN dbo.AssessmentDevelopmentProject ON dbo.AssessmentDevelopmentProject.id = dbo.Project.Id
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche ON ProjectFiche.Project_ID = dbo.Project.LegacyId
	INNER JOIN ACTSERVER.ACT.dbo.ProjFiche_CompProf CompetenceProfile ON CompetenceProfile.ACProject_ID = ProjectFiche.ACProject_ID
	INNER JOIN ACTSERVER.ACT.dbo.W_Samengesteld ActDictionary ON ActDictionary.W_Samengesteld_ID = CompetenceProfile.W_Samengesteld_ID
	INNER JOIN dbo.DictionaryIndicator ON dbo.DictionaryIndicator.LegacyId = ActDictionary.Indicator_ID
	INNER JOIN dbo.DictionaryLevel ON dbo.DictionaryLevel.Id = dbo.DictionaryIndicator.DictionaryLevelId
	INNER JOIN dbo.DictionaryCompetence ON dbo.DictionaryCompetence.Id = dbo.DictionaryLevel.DictionaryCompetenceId
	INNER JOIN dbo.DictionaryCluster ON dbo.DictionaryCluster.Id = dbo.DictionaryCompetence.DictionaryClusterId AND dbo.DictionaryCluster.DictionaryId = dbo.AssessmentDevelopmentProject.DictionaryId

--Development Center
INSERT INTO ProjectCategoryDetail2DictionaryIndicator(Id, ProjectCategoryDetailId, DictionaryIndicatorId, IsDefinedByRole, IsStandard, IsDistinctive)	
	SELECT	NEWID(),
			dbo.ProjectCategoryDetail.Id,
			dbo.DictionaryIndicator.Id,
			0,
			dbo.DictionaryIndicator.IsStandard,
			dbo.DictionaryIndicator.IsDistinctive
			
	FROM dbo.ProjectCategoryDcDetail
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryDcDetail.Id = dbo.ProjectCategoryDetail.Id
	INNER JOIN dbo.Project ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN dbo.AssessmentDevelopmentProject ON dbo.AssessmentDevelopmentProject.id = dbo.Project.Id
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche ON ProjectFiche.Project_ID = dbo.Project.LegacyId
	INNER JOIN ACTSERVER.ACT.dbo.ProjFiche_CompProf CompetenceProfile ON CompetenceProfile.ACProject_ID = ProjectFiche.ACProject_ID
	INNER JOIN ACTSERVER.ACT.dbo.W_Samengesteld ActDictionary ON ActDictionary.W_Samengesteld_ID = CompetenceProfile.W_Samengesteld_ID
	INNER JOIN dbo.DictionaryIndicator ON dbo.DictionaryIndicator.LegacyId = ActDictionary.Indicator_ID
	INNER JOIN dbo.DictionaryLevel ON dbo.DictionaryLevel.Id = dbo.DictionaryIndicator.DictionaryLevelId
	INNER JOIN dbo.DictionaryCompetence ON dbo.DictionaryCompetence.Id = dbo.DictionaryLevel.DictionaryCompetenceId
	INNER JOIN dbo.DictionaryCluster ON dbo.DictionaryCluster.Id = dbo.DictionaryCompetence.DictionaryClusterId AND dbo.DictionaryCluster.DictionaryId = dbo.AssessmentDevelopmentProject.DictionaryId

--Executive Assessment
INSERT INTO ProjectCategoryDetail2DictionaryIndicator(Id, ProjectCategoryDetailId, DictionaryIndicatorId, IsDefinedByRole, IsStandard, IsDistinctive)	
	SELECT	NEWID(),
			dbo.ProjectCategoryDetail.Id,
			dbo.DictionaryIndicator.Id,
			0,
			dbo.DictionaryIndicator.IsStandard,
			dbo.DictionaryIndicator.IsDistinctive
			
	FROM dbo.ProjectCategoryEaDetail
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryEaDetail.Id = dbo.ProjectCategoryDetail.Id
	INNER JOIN dbo.Project ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN dbo.AssessmentDevelopmentProject ON dbo.AssessmentDevelopmentProject.id = dbo.Project.Id
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche ON ProjectFiche.Project_ID = dbo.Project.LegacyId
	INNER JOIN ACTSERVER.ACT.dbo.ProjFiche_CompProf CompetenceProfile ON CompetenceProfile.ACProject_ID = ProjectFiche.ACProject_ID
	INNER JOIN ACTSERVER.ACT.dbo.W_Samengesteld ActDictionary ON ActDictionary.W_Samengesteld_ID = CompetenceProfile.W_Samengesteld_ID
	INNER JOIN dbo.DictionaryIndicator ON dbo.DictionaryIndicator.LegacyId = ActDictionary.Indicator_ID
	INNER JOIN dbo.DictionaryLevel ON dbo.DictionaryLevel.Id = dbo.DictionaryIndicator.DictionaryLevelId
	INNER JOIN dbo.DictionaryCompetence ON dbo.DictionaryCompetence.Id = dbo.DictionaryLevel.DictionaryCompetenceId
	INNER JOIN dbo.DictionaryCluster ON dbo.DictionaryCluster.Id = dbo.DictionaryCompetence.DictionaryClusterId AND dbo.DictionaryCluster.DictionaryId = dbo.AssessmentDevelopmentProject.DictionaryId

--Focused Assessment
INSERT INTO ProjectCategoryDetail2DictionaryIndicator(Id, ProjectCategoryDetailId, DictionaryIndicatorId, IsDefinedByRole, IsStandard, IsDistinctive)	
	SELECT	NEWID(),
			dbo.ProjectCategoryDetail.Id,
			dbo.DictionaryIndicator.Id,
			CASE WHEN ProjectRole2DictionaryIndicator.ProjectRoleId IS NULL THEN 0 ELSE 1 END,			
			CASE WHEN dbo.ProjectRole2DictionaryIndicator.Norm = 10 THEN 1 ELSE 0 END,
			CASE WHEN dbo.ProjectRole2DictionaryIndicator.Norm = 20 THEN 1 ELSE 0 END
			
	FROM dbo.ProjectCategoryFaDetail
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryFaDetail.Id = dbo.ProjectCategoryDetail.Id
	INNER JOIN dbo.Project ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN dbo.AssessmentDevelopmentProject ON dbo.AssessmentDevelopmentProject.id = dbo.Project.Id
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche ON ProjectFiche.Project_ID = dbo.Project.LegacyId
	INNER JOIN ACTSERVER.ACT.dbo.ProjFiche_CompProf CompetenceProfile ON CompetenceProfile.ACProject_ID = ProjectFiche.ACProject_ID
	INNER JOIN ACTSERVER.ACT.dbo.W_Samengesteld ActDictionary ON ActDictionary.W_Samengesteld_ID = CompetenceProfile.W_Samengesteld_ID
	INNER JOIN dbo.DictionaryIndicator ON dbo.DictionaryIndicator.LegacyId = ActDictionary.Indicator_ID
	INNER JOIN dbo.DictionaryLevel ON dbo.DictionaryLevel.Id = dbo.DictionaryIndicator.DictionaryLevelId
	INNER JOIN dbo.DictionaryCompetence ON dbo.DictionaryCompetence.Id = dbo.DictionaryLevel.DictionaryCompetenceId
	INNER JOIN dbo.DictionaryCluster ON dbo.DictionaryCluster.Id = dbo.DictionaryCompetence.DictionaryClusterId AND dbo.DictionaryCluster.DictionaryId = dbo.AssessmentDevelopmentProject.DictionaryId	
	LEFT JOIN dbo.ProjectRole2DictionaryIndicator ON dbo.ProjectRole2DictionaryIndicator.ProjectRoleId = dbo.ProjectCategoryFaDetail.ProjectRoleId AND dbo.ProjectRole2DictionaryIndicator.DictionaryIndicatorId = dbo.DictionaryIndicator.Id

--Focused Assessment
INSERT INTO ProjectCategoryDetail2DictionaryIndicator(Id, ProjectCategoryDetailId, DictionaryIndicatorId, IsDefinedByRole, IsStandard, IsDistinctive)	
	SELECT	NEWID(),
			dbo.ProjectCategoryDetail.Id,
			dbo.DictionaryIndicator.Id,
			CASE WHEN ProjectRole2DictionaryIndicator.ProjectRoleId IS NULL THEN 0 ELSE 1 END,			
			CASE WHEN dbo.ProjectRole2DictionaryIndicator.Norm = 10 THEN 1 ELSE 0 END,
			CASE WHEN dbo.ProjectRole2DictionaryIndicator.Norm = 20 THEN 1 ELSE 0 END
			
	FROM dbo.ProjectCategoryFdDetail
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.ProjectCategoryFdDetail.Id = dbo.ProjectCategoryDetail.Id
	INNER JOIN dbo.Project ON dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
	INNER JOIN dbo.AssessmentDevelopmentProject ON dbo.AssessmentDevelopmentProject.id = dbo.Project.Id
	INNER JOIN ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche ON ProjectFiche.Project_ID = dbo.Project.LegacyId
	INNER JOIN ACTSERVER.ACT.dbo.ProjFiche_CompProf CompetenceProfile ON CompetenceProfile.ACProject_ID = ProjectFiche.ACProject_ID
	INNER JOIN ACTSERVER.ACT.dbo.W_Samengesteld ActDictionary ON ActDictionary.W_Samengesteld_ID = CompetenceProfile.W_Samengesteld_ID
	INNER JOIN dbo.DictionaryIndicator ON dbo.DictionaryIndicator.LegacyId = ActDictionary.Indicator_ID
	INNER JOIN dbo.DictionaryLevel ON dbo.DictionaryLevel.Id = dbo.DictionaryIndicator.DictionaryLevelId
	INNER JOIN dbo.DictionaryCompetence ON dbo.DictionaryCompetence.Id = dbo.DictionaryLevel.DictionaryCompetenceId
	INNER JOIN dbo.DictionaryCluster ON dbo.DictionaryCluster.Id = dbo.DictionaryCompetence.DictionaryClusterId AND dbo.DictionaryCluster.DictionaryId = dbo.AssessmentDevelopmentProject.DictionaryId	
	LEFT JOIN dbo.ProjectRole2DictionaryIndicator ON dbo.ProjectRole2DictionaryIndicator.ProjectRoleId = dbo.ProjectCategoryFdDetail.ProjectRoleId AND dbo.ProjectRole2DictionaryIndicator.DictionaryIndicatorId = dbo.DictionaryIndicator.Id