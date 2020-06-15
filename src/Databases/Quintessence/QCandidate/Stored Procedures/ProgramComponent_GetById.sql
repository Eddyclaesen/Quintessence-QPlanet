CREATE PROCEDURE [QCandidate].[ProgramComponent_GetById]
	@id UNIQUEIDENTIFIER
AS

	SET NOCOUNT ON;

SELECT
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
	dbo.ProgramComponent prc
	LEFT OUTER JOIN dbo.[User] uLeadAssess WITH (NOLOCK)
		ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT OUTER JOIN dbo.[User] uCoAssess WITH (NOLOCK)
		ON uCoAssess.Id = prc.CoAssessorUserId
	LEFT OUTER JOIN (dbo.SimulationCombination sc WITH (NOLOCK)
		INNER JOIN dbo.Simulation s WITH (NOLOCK)
			ON s.Id = sc.SimulationId)
		ON sc.Id = prc.SimulationCombinationId	
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK)
		ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK)
		ON o.Id = ar.OfficeId
WHERE
	prc.Id = @id