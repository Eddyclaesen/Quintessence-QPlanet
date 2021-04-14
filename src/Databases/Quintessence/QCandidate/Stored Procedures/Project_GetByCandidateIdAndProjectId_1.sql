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
			@Co AS CoAssessor
FROM		dbo.ProjectCandidateView pv
LEFT JOIN	dbo.AssessmentDevelopmentProjectView ac on pv.ProjectId = ac.Id
LEFT JOIN	dbo.ProjectView p on pv.ProjectId = p.Id
LEFT JOIN	dbo.CrmReplicationAppointment crm on pv.CrmCandidateAppointmentId = crm.Id
LEFT JOIN	dbo.CrmContactView contact on p.ContactId = contact.Id
LEFT JOIN	ProjectTypeCategoryView pt on ac.ProjectTypeCategoryCode = pt.Code
LEFT JOIN	OfficeView o on crm.OfficeId = o.Id
LEFT JOIN	@C context on p.Id = context.ProjectId
WHERE		pv.CandidateId = @candidateId
	AND		pv.ProjectId = @projectId