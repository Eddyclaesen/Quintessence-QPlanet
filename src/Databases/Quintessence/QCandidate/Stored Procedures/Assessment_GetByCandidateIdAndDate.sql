CREATE PROCEDURE [QCandidate].[Assessment_GetByCandidateIdAndDate]
	@candidateId UNIQUEIDENTIFIER,
	@date DATE
AS

	SET NOCOUNT ON;

SELECT
	--Customer
		cc.Id,
		cc.Name,

	--Position
		p.Id,
		p.Name,

	--DayProgram
		--Location
			o.Id,
			o.FullName AS Name,
		--ProgramComponents
			prc.Id,
			prc.[Start],
			prc.[End],
			s.[Name],
			prc.Description,
			prc.SimulationCombinationId,
			--Room
				ar.Id,
				ar.[Name],
			--LeadAssessor
				uLeadAssess.Id,
				uLeadAssess.FirstName,
				uLeadAssess.LastName,
			--CoAssessor
				uCoAssess.Id,
				uCoAssess.FirstName,
				uCoAssess.LastName			
FROM
	dbo.candidate c WITH (NOLOCK)
	INNER JOIN dbo.ProjectCandidate pc WITH (NOLOCK) ON pc.CandidateId = c.Id
	INNER JOIN dbo.Project p WITH (NOLOCK) ON p.Id = pc.ProjectId
	INNER JOIN dbo.CrmContact cc WITH (NOLOCK) ON cc.Id = p.ContactId
	INNER JOIN dbo.ProgramComponent prc WITH (NOLOCK) ON prc.ProjectCandidateId = pc.Id
	LEFT JOIN dbo.[User] uLeadAssess WITH (NOLOCK) ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT JOIN dbo.[User] uCoAssess WITH (NOLOCK) ON uCoAssess.Id = prc.CoAssessorUserId
	LEFT JOIN dbo.SimulationCombination sc WITH (NOLOCK) ON sc.Id = prc.SimulationCombinationId
	LEFT JOIN dbo.Simulation s WITH (NOLOCK) ON s.Id = sc.SimulationId
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK) ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK) ON o.Id = ar.OfficeId
WHERE
	c.Id = @candidateId
	AND CONVERT(DATE, prc.Start) = @date
	AND prc.Description NOT LIKE '%Input scoring%'
	AND CONVERT(VARCHAR, prc.Description) NOT IN('Preparation consultant','Assessor debriefing','Proma','Assessor debriefing GGI')
ORDER BY prc.Start, prc.[End]