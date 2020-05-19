CREATE PROCEDURE [dbo].[StartSingleNeoQplanet]
	@CandidateId	NVARCHAR(MAX)
AS
BEGIN
	DECLARE @CrmParticipantId INT
	DECLARE @FirstName nvarchar(max)
	DECLARE @LastName nvarchar(max)
	DECLARE @LanguageCode NVARCHAR(2)
	DECLARE @CategoryCode NVARCHAR(20)
	DECLARE @Password NVARCHAR(25)	

	set @CategoryCode = 'NEOPIR'
	set @CrmParticipantId = (select LegacyId from Quintessence.dbo.Candidate where Id = @CandidateId)
	set @FirstName = (select FirstName from Quintessence.dbo.Candidate where Id = @CandidateId)
	set @LastName = (select LastName from Quintessence.dbo.Candidate where Id = @CandidateId)
	set @LanguageCode = (select CASE LanguageId 
									WHEN 1 THEN 'NL'
									WHEN 2 THEN 'FR'
									WHEN 3 THEN 'EN'
									WHEN 4 THEN 'DE'
								END	
						from Quintessence.dbo.Candidate where Id = @CandidateId)

	EXECUTE [Superoffice7].[dbo].[QPlanet_GenerateType3Code] @CrmParticipantId, 
															 @FirstName, 
															 @LastName, 
															 @LanguageCode,
															 @CategoryCode,
															 @Password OUTPUT	

	UPDATE Quintessence.dbo.Candidate
	SET Reference = @Password
	WHERE Id = @CandidateId	
END
