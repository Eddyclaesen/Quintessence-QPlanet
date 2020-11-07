CREATE PROCEDURE [QCandidate].[Assessment_GetByCandidateIdAndDateAndLanguage]
	@candidateId UNIQUEIDENTIFIER,
	@date DATE,
	@language char(2)
AS

	SET NOCOUNT ON;
	DECLARE @LanguageId int
	SET @LanguageId = case @language
		when 'nl' then 1
		when 'fr' then 2
		else 3
	end

SELECT
	--Customer
		cc.Id,
		cc.Name,

	--Position
		p.Id,
		p.Name,

	--DayProgram
		@date AS Date,
		--Location
			o.Id,
			o.FullName AS Name,
		--ProgramComponents
			prc.Id,
			prc.[Start],
			prc.[End],
			CASE 
					WHEN (uLeadAssess.FirstName is null and sc.Preparation > 0) THEN s.[Name]+ CASE @LanguageId 
																									WHEN 1 THEN ' (voorbereiding)'
																									WHEN 2 THEN ' (préparation)'
																									ELSE ' (preparation)'
																									END
					WHEN (uLeadAssess.FirstName is not null and sc.Execution > 0) THEN s.[Name]+ CASE @LanguageId
																									WHEN 1 THEN ' (uitvoering)'
																									WHEN 2 THEN ' (execution)'
																									ELSE ' (execution)'
																									END
					ELSE s.[Name]
					END AS [Name],
			prc.Description,
			prc.SimulationCombinationId,
			ISNULL(sc.QCandidateLayoutId, 0) AS QCandidateLayoutId,
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
				--sc.Preparation,
				--sc.Execution		
FROM
	dbo.candidate c WITH (NOLOCK)
	INNER JOIN dbo.ProjectCandidate pc WITH (NOLOCK)
		ON pc.CandidateId = c.Id
	INNER JOIN dbo.Project p WITH (NOLOCK)
		ON p.Id = pc.ProjectId
	INNER JOIN dbo.CrmContact cc WITH (NOLOCK)
		ON cc.Id = p.ContactId
	INNER JOIN dbo.ProgramComponent prc WITH (NOLOCK)
		ON prc.ProjectCandidateId = pc.Id
	LEFT OUTER JOIN dbo.[User] uLeadAssess WITH (NOLOCK)
		ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT OUTER JOIN dbo.[User] uCoAssess WITH (NOLOCK)
		ON uCoAssess.Id = prc.CoAssessorUserId
	LEFT OUTER JOIN (dbo.SimulationCombination sc WITH (NOLOCK)
		INNER JOIN dbo.SimulationTranslationView s WITH (NOLOCK)
			ON s.SimulationId = sc.SimulationId)
		ON sc.Id = prc.SimulationCombinationId	
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK)
		ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK)
		ON o.Id = ar.OfficeId
WHERE
	c.Id = @candidateId
	AND CONVERT(DATE, prc.Start) = @date
	--AND prc.Description NOT LIKE '%Input scoring%'
	--AND CONVERT(VARCHAR, prc.Description) NOT IN('Preparation consultant','Assessor debriefing','Proma','Assessor debriefing GGI')
	AND ISNULL(prc.Description,'') NOT LIKE '%Input scoring%'
	AND CONVERT(VARCHAR, ISNULL(prc.Description,'')) NOT IN ('Preparation consultant','Assessor debriefing','Proma','Assessor debriefing GGI')
	AND ISNULL(s.LanguageId, @LanguageId) = @LanguageId
	AND prc.Audit_IsDeleted = 0
ORDER BY
	prc.Start,
	prc.[End]