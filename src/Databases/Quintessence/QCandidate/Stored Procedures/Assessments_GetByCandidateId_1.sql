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