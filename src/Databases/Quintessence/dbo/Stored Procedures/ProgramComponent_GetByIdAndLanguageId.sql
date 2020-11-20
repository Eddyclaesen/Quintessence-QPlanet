CREATE PROCEDURE [dbo].[ProgramComponent_GetByIdAndLanguageId]
	@id UNIQUEIDENTIFIER,
	@languageId INT
AS

SET NOCOUNT ON;

SELECT
	--ProgramComponents
	prc.Id,
	prc.[Start],
	prc.[End],
	ST.[Name] AS [Name],
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
FROM dbo.ProgramComponent prc
	LEFT OUTER JOIN dbo.[User] uLeadAssess WITH (NOLOCK)
		ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT OUTER JOIN dbo.[User] uCoAssess WITH (NOLOCK)
		ON uCoAssess.Id = prc.CoAssessorUserId
	INNER JOIN dbo.SimulationCombination SC
		ON SC.Id = PRC.SimulationCombinationId
	INNER JOIN dbo.SimulationTranslation ST WITH (NOLOCK)
		ON ST.SimulationId = SC.SimulationId
			AND ST.LanguageId = @languageId
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK)
		ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK)
		ON o.Id = ar.OfficeId
WHERE
	prc.Id = @id