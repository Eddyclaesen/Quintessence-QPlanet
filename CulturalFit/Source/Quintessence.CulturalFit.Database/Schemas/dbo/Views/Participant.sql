CREATE VIEW [dbo].[Participant]
	AS 
SELECT	KandInfo.Kand_Inf_ID AS Id,
		KandInfo.Voornaam AS FirstName,
		KandInfo.Naam AS LastName,
		KandInfo.Tijd AS Deadline,
		KandInfo.ACProject_ID AS ProjectId,
		[TheoremListRequest].[Id] AS TheoremListRequestId,
		[TheoremListRequest].[VerificationCode] AS TheoremListVerificationCode,
		[TheoremListRequest].[RequestDate] AS TheoremListRequestDate

FROM	[$(SuperOffice7Server)].[$(SuperOffice7)].[dbo].[kand_inf] KandInfo

LEFT JOIN	[CulturalFitCandidateLink]
	ON		[CulturalFitCandidateLink].[ParticipantId] = KandInfo.[Kand_Inf_ID]
LEFT JOIN	[TheoremListRequest]
	ON		[TheoremListRequest].[TheoremListCandidateId] = [CulturalFitCandidateLink].[TheoremListCandidateId]

WHERE ACProject_ID IS NOT NULL