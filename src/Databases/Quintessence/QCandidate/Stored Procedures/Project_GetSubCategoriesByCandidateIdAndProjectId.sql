CREATE PROCEDURE [QCandidate].[Project_GetSubCategoriesByCandidateIdAndProjectId]
       @projectId UNIQUEIDENTIFIER,
	   @candidateId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON;

select pc.[Name]
,NULL AS LoginCode
,NULL AS EndDate
,pt.Code
,CASE pt.Code
WHEN 'MOTIVATION' THEN 
	CASE pv.CandidateLanguageId
		WHEN 1 THEN 'https://talentcentral.eu.shl.com/player/link/c21605e7d3e34aa7842d5d6ef0c62259'
		WHEN 2 THEN 'https://talentcentral.eu.shl.com/player/link/80feac2ba0ad4aad8f6d9f5b4c769da0'
		ELSE 'https://talentcentral.eu.shl.com/player/link/c21605e7d3e34aa7842d5d6ef0c62259'
	END	
ELSE NULL
END AS LoginLink
,0 AS Completed
from [dbo].[ProjectCategoryDetailType2View] pc
left join [dbo].[ProjectCategoryDetailView] pd on pc.Id = pd.Id
left join [dbo].[ProjectTypeCategoryView] pt on pd.ProjectTypeCategoryId = pt.Id
left join dbo.[ProjectCandidateCategoryDetailType2View] c on pc.Id = c.ProjectCategoryDetailTypeId
left join dbo.ProjectCandidateView pv on c.ProjectCandidateId = pv.Id
where pd.ProjectId = @ProjectId
and pv.CandidateId = @CandidateId
and pc.SurveyPlanningId = 2
UNION ALL
select pc.[Name]
,c.LoginCode
,test.EndDate
,pt.Code
,CASE pt.Code
WHEN 'NEOPIR' THEN 'https://personality.quintessence.be'+'?code='+ISNULL(c.LoginCode,'')
WHEN 'LEIDERSTIJ' THEN 'https://personality.quintessence.be.'+'?code='+ISNULL(c.LoginCode,'')
ELSE NULL
END AS LoginUrl
,CASE WHEN test.EndDate is not null then 1
ELSE 0
END AS Completed
from [dbo].[ProjectCategoryDetailType3View] pc
left join [dbo].[ProjectCategoryDetailView] pd on pc.Id = pd.Id
left join [dbo].[ProjectTypeCategoryView] pt on pd.ProjectTypeCategoryId = pt.Id
left join [dbo].[ProjectCandidateCategoryDetailType3View] c on pc.Id = c.ProjectCategoryDetailTypeId
left join dbo.ProjectCandidateView pv on c.ProjectCandidateId = pv.Id
left join Qata.dbo.Session qata on c.LoginCode COLLATE DATABASE_DEFAULT = qata.Code COLLATE DATABASE_DEFAULT
left join Qata.dbo.Test test on qata.SessionID = test.SessionID
where pd.ProjectId = @ProjectId
and pv.CandidateId = @CandidateId
and pc.SurveyPlanningId = 2
