CREATE PROCEDURE [QCandidate].[Assessments_GetByCandidateId]
       @candidateId UNIQUEIDENTIFIER
AS

       SET NOCOUNT ON;

SELECT  p.Id AS ProjectId
		,case c.LanguageId
			when 1 then ac.FunctionTitle
			when 2 then ISNULL(ac.FunctionTitleFR,ac.FunctionTitle)
			when 3 then ISNULL(ac.FunctionTitleEN,ac.FunctionTitle)
			else ac.FunctionTitle
		end AS FunctionTitle
		,crm.AppointmentDate
		,pc.IsCancelled		
FROM	dbo.candidate c WITH (NOLOCK)
		INNER JOIN dbo.ProjectCandidate pc WITH (NOLOCK) ON pc.CandidateId = c.Id
		INNER JOIN dbo.Project p WITH (NOLOCK) ON p.Id = pc.ProjectId
		INNER JOIN dbo.CrmContact cc WITH (NOLOCK) ON cc.Id = p.ContactId
		LEFT JOIN CrmReplicationAppointment crm on pc.CrmCandidateAppointmentId = crm.Id
		LEFT JOIN AssessmentDevelopmentProjectView ac on p.Id = ac.Id
WHERE
       c.Id = @candidateId
ORDER BY 
	   crm.AppointmentDate DESC
GO

CREATE PROCEDURE [QCandidate].[Project_GetByCandidateIdAndProjectId]
       @projectId UNIQUEIDENTIFIER,
	   @candidateId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON;

DECLARE @Lead NVARCHAR(MAX)
DECLARE @Co NVARCHAR(MAX)

set @Lead = (select TOP(1) u.FirstName+' '+u.LastName
			from ProjectCandidateView pv
			left join CrmReplicationAppointment crm on pv.CrmCandidateAppointmentId = crm.Id
			left join UserView u on crm.AssociateId = u.AssociateId
			where pv.CandidateId = @candidateId and pv.ProjectId = @projectId)

set @Co = (select TOP(1) u.FirstName+' '+u.LastName
			from ProjectCandidateView pv
			left join CrmReplicationAppointment crm on pv.Code = crm.Code
			left join UserView u on crm.AssociateId = u.AssociateId
			where crm.TaskId = 190
			and pv.CandidateId = @candidateId 
			and pv.ProjectId = @projectId)

DECLARE @Contexts TABLE (Id UNIQUEIDENTIFIER, SimulationContextId UNIQUEIDENTIFIER, [Type] CHAR(2))
INSERT INTO @Contexts
	SELECT
		[Id],
		SimulationContextId, 'AC' AS [Type]
	FROM [dbo].[ProjectCategoryAcDetailView] WITH (NOLOCK)
	
	UNION ALL

    SELECT
		[Id],
		SimulationContextId, 'CA' AS [Type]
	FROM [dbo].[ProjectCategoryCaDetailView] WITH (NOLOCK)
    
	UNION ALL

    SELECT 
		[Id],
		SimulationContextId, 'DC' AS [Type]
	FROM [dbo].[ProjectCategoryDcDetailView] WITH (NOLOCK)

    UNION ALL

    SELECT 
		[Id],
		SimulationContextId, 'EA' AS [Type]
	FROM [dbo].[ProjectCategoryEaDetailView] WITH (NOLOCK)
    
	UNION ALL

    SELECT
		[Id],
		SimulationContextId, 'FA' AS [Type]
	FROM [dbo].[ProjectCategoryFaDetailView] WITH (NOLOCK)
    
	UNION ALL
    
	SELECT 
		[Id],
		SimulationContextId, 'FD' AS [Type]
	FROM [dbo].[ProjectCategoryFdDetailView] WITH (NOLOCK)
    
	UNION ALL
    
	SELECT 
		[Id],
		SimulationContextId, 'PS' AS [Type]
	FROM [dbo].[ProjectCategoryPsDetailView] WITH (NOLOCK)
    
	UNION ALL
    
	SELECT 
		[Id],
		SimulationContextId, 'PS' AS [Type]
	FROM [dbo].[ProjectCategorySoDetailView] WITH (NOLOCK)


DECLARE @C TABLE (ProjectId UNIQUEIDENTIFIER, SimulationContextId UNIQUEIDENTIFIER, Context NVARCHAR(MAX))
insert into @C
SELECT @ProjectId AS ProjectId
	,C.[SimulationContextId]
	,SC.[Name]
FROM dbo.ProjectView PC1 WITH (NOLOCK)
	INNER JOIN [dbo].[ProjectCategoryDetailView] PCDV WITH (NOLOCK)
		ON PCDV.[ProjectId] = PC1.[Id] 
	INNER JOIN [dbo].[ProjectTypeCategoryView] PTC  WITH (NOLOCK)
		ON PCDV.ProjectTypeCategoryId = PTC.[Id]
	INNER JOIN @Contexts C      
		ON PTC.[Code] = C.[Type]
			AND C.[Id] = PCDV.[Id]
	INNER JOIN SimulationContextView SC
		ON C.SimulationContextId = SC.Id
WHERE PC1.[Id] = @ProjectId

SELECT		p.Id AS ProjectId,
			CASE pv.CandidateLanguageId
				WHEN 1 THEN ac.FunctionTitle
				WHEN 2 THEN ISNULL(ac.FunctionTitleFR,ac.FunctionTitle)
				WHEN 3 THEN ISNULL(ac.FunctionTitleEN,ac.FunctionTitle)
			END AS FunctionTitle,
			contact.Name AS Company,
			crm.AppointmentDate,
			pt.[Name] AS AssessmentType,
			CASE pv.OnlineAssessment
				WHEN 1 THEN 'ON'
				ELSE o.ShortName				
			END AS [Location],
			context.Context,
			@Lead AS LeadAssessor,
			@Co AS CoAssessor, 
			cuv.UserName AS ContextUserName,
			cuv.Password AS ContextPassword
FROM		dbo.ProjectCandidateView pv
LEFT JOIN	dbo.AssessmentDevelopmentProjectView ac on pv.ProjectId = ac.Id
LEFT JOIN	dbo.ProjectView p on pv.ProjectId = p.Id
LEFT JOIN	dbo.CrmReplicationAppointment crm on pv.CrmCandidateAppointmentId = crm.Id
LEFT JOIN	dbo.CrmContactView contact on p.ContactId = contact.Id
LEFT JOIN	ProjectTypeCategoryView pt on ac.ProjectTypeCategoryCode = pt.Code
LEFT JOIN	OfficeView o on crm.OfficeId = o.Id
LEFT JOIN	@C context on p.Id = context.ProjectId
left join	SimulationContextUserView cuv on context.SimulationContextId = cuv.SimulationContextId
WHERE		pv.CandidateId = @candidateId
	AND		pv.ProjectId = @projectId
GO

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
GO

ALTER TABLE ProjectCandidate 
ADD [Consent] BIT NOT NULL DEFAULT 0;
GO

CREATE PROCEDURE [QCandidate].[SetConsentByCandidateIdAndProjectId]
	@check BIT,
	@candidateid UNIQUEIDENTIFIER,
	@projectid UNIQUEIDENTIFIER
AS
	UPDATE	ProjectCandidate
	SET		Consent		= @check
	WHERE	CandidateId = @candidateid
		AND ProjectId	= @projectid
GO

