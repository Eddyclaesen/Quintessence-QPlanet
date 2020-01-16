BEGIN TRAN
DELETE FROM dbo.ProjectCandidate
DELETE FROM dbo.Project2CrmProject
DELETE FROM dbo.ProjectCategoryAcDetail
DELETE FROM dbo.ProjectCategoryDcDetail
DELETE FROM dbo.ProjectCategoryEaDetail
DELETE FROM dbo.ProjectCategoryFaDetail
DELETE FROM dbo.ProjectCategoryFdDetail
DELETE FROM dbo.ProjectCategoryCaDetail
DELETE FROM dbo.ProjectCategoryDetail
DELETE FROM dbo.AssessmentDevelopmentProject
DELETE FROM dbo.Project

DECLARE		@ACDCId AS UNIQUEIDENTIFIER
SELECT		@ACDCId = Id
FROM		[ProjectTypeView]
WHERE		[Code] = 'ACDC'

INSERT INTO [Project]([Id], [ProjectTypeId], [Name], [ContactId], [ProjectManagerId], [CustomerAssistantId], [PricingModelId], [StatusCode], [LegacyId])
	SELECT		NEWID(),
				@ACDCId,
				ProjectToMigrate.Name,
				ProjectToMigrate.ContactId,
				ProjectManagerUser.Id,
				CustomerAssistantUser.Id,
				1,
				10,
				ProjectToMigrate.Id

	FROM		ACTSERVER.ACT.dbo.ProjectToMigrate ProjectToMigrate
	
	LEFT JOIN	dbo.[User] ProjectManagerUser
		ON		ProjectManagerUser.AssociateId = ProjectToMigrate.ProjectManagerAssociateId

	LEFT JOIN	dbo.[User] CustomerAssistantUser
		ON		CustomerAssistantUser.AssociateId = ProjectToMigrate.CustomerAssistantAssociateId

INSERT INTO [dbo].[AssessmentDevelopmentProject]([Id], [FunctionTitle], [FunctionInformation], [DictionaryId], CandidateScoreReportTypeId, IsRevisionByPmRequired, SendReportToParticipant)
	SELECT		Project.Id,
				ProjectToMigrate.FunctionTitle,
				ProjectToMigrate.FunctionInformation,
				dbo.Dictionary.Id,
				1,
				1,
				0				
				
	FROM		Project
	
	INNER JOIN	ACTSERVER.ACT.dbo.ProjectToMigrate ProjectToMigrate
		ON		ProjectToMigrate.Id = dbo.Project.LegacyId

	LEFT JOIN	dbo.Dictionary
		ON		dbo.Dictionary.LegacyId = ProjectToMigrate.DictionaryLegacyId

INSERT INTO	dbo.Project2CrmProject(ProjectId, CrmProjectId)
	SELECT		Project.Id, ProjectToMigrate.CrmProjectId				
				
	FROM		Project
	
	INNER JOIN	ACTSERVER.ACT.dbo.ProjectToMigrate ProjectToMigrate
		ON		ProjectToMigrate.Id = dbo.Project.LegacyId
		AND		ProjectToMigrate.CrmProjectId IS NOT NULL

INSERT INTO	dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT		NEWID(),
				Project.Id,
				ProjectTypeCategory.Id,
				0				
	FROM		dbo.Project
	
	INNER JOIN	ACTSERVER.ACT.dbo.ProjectToMigrate ProjectToMigrate
		ON		ProjectToMigrate.Id = dbo.Project.LegacyId

	INNER JOIN	dbo.ProjectTypeCategory
		ON		ProjectTypeCategory.Code COLLATE Database_Default = ProjectToMigrate.ProjectTypeCode COLLATE database_default

INSERT INTO dbo.ProjectCategoryAcDetail (Id, ScoringTypeCode)
	SELECT		dbo.ProjectCategoryDetail.Id,
				1	
	
	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		ProjectTypeCategory.Id = dbo.ProjectCategoryDetail.ProjectTypeCategoryId
		AND		ProjectTypeCategory.Code = 'AC'

INSERT INTO dbo.ProjectCategoryDcDetail (Id, ScoringTypeCode)
	SELECT		dbo.ProjectCategoryDetail.Id,
				1
	
	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		ProjectTypeCategory.Id = dbo.ProjectCategoryDetail.ProjectTypeCategoryId
		AND		ProjectTypeCategory.Code = 'DC'

INSERT INTO dbo.ProjectCategoryEaDetail (Id, ScoringTypeCode)
	SELECT		dbo.ProjectCategoryDetail.Id,
				1
	
	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		ProjectTypeCategory.Id = dbo.ProjectCategoryDetail.ProjectTypeCategoryId
		AND		ProjectTypeCategory.Code = 'EA'

INSERT INTO dbo.ProjectCategoryFaDetail (Id, ScoringTypeCode, ProjectRoleId)
	SELECT		dbo.ProjectCategoryDetail.Id,
				1,
				dbo.ProjectRole.Id
	
	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		ProjectTypeCategory.Id = dbo.ProjectCategoryDetail.ProjectTypeCategoryId
		AND		ProjectTypeCategory.Code = 'FA'

	INNER JOIN	dbo.Project
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id

	INNER JOIN  ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche
		ON		ProjectFiche.Project_ID = dbo.Project.LegacyId

	INNER JOIN	dbo.ProjectRole
		ON		dbo.ProjectRole.Legacy6Id = ProjectFiche.FA_Projecttype_ID

INSERT INTO dbo.ProjectCategoryFdDetail (Id, ScoringTypeCode, ProjectRoleId)
	SELECT		dbo.ProjectCategoryDetail.Id,
				1,
				dbo.ProjectRole.Id
	
	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		ProjectTypeCategory.Id = dbo.ProjectCategoryDetail.ProjectTypeCategoryId
		AND		ProjectTypeCategory.Code = 'FD'

	INNER JOIN	dbo.Project
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id

	INNER JOIN  ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche
		ON		ProjectFiche.Project_ID = dbo.Project.LegacyId

	INNER JOIN	dbo.ProjectRole
		ON		dbo.ProjectRole.Legacy14Id = ProjectFiche.FA_Projecttype_ID
		
INSERT INTO dbo.ProjectCategoryCaDetail (Id)
	SELECT ProjectCategoryDetail.Id FROM dbo.Project
	INNER JOIN dbo.ProjectCategoryDetail ON dbo.Project.Id = dbo.ProjectCategoryDetail.ProjectId
	LEFT JOIN dbo.ProjectCategoryAcDetail ON ProjectCategoryAcDetail.Id = dbo.ProjectCategoryDetail.Id
	LEFT JOIN dbo.ProjectCategoryDcDetail ON ProjectCategoryDcDetail.Id = dbo.ProjectCategoryDetail.Id
	LEFT JOIN dbo.ProjectCategoryFaDetail ON ProjectCategoryFaDetail.Id = dbo.ProjectCategoryDetail.Id
	LEFT JOIN dbo.ProjectCategoryFdDetail ON ProjectCategoryFdDetail.Id = dbo.ProjectCategoryDetail.Id
	LEFT JOIN dbo.ProjectCategoryEaDetail ON ProjectCategoryEaDetail.Id = dbo.ProjectCategoryDetail.Id
	WHERE	dbo.ProjectCategoryAcDetail.Id IS NULL
	AND		dbo.ProjectCategoryDcDetail.Id IS NULL
	AND		dbo.ProjectCategoryFaDetail.Id IS NULL
	AND		dbo.ProjectCategoryFdDetail.Id IS NULL
	AND		dbo.ProjectCategoryEaDetail.Id IS NULL
COMMIT