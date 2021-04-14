CREATE PROCEDURE [QCandidate].[Project_GetByCandidateIdAndProjectId]
       @projectId UNIQUEIDENTIFIER,
	   @candidateId UNIQUEIDENTIFIER
AS
       SET NOCOUNT ON;

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