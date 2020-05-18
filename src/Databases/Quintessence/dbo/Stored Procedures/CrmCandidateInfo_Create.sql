CREATE PROCEDURE [dbo].[CrmCandidateInfo_Create]
	@FirstName	NVARCHAR(MAX),
	@LastName	NVARCHAR(MAX)
AS
BEGIN
	DECLARE @CandidateInfoId INT
	EXECUTE [Superoffice7].dbo.[QPlanet_InsertCandidateInfo] @FirstName, @LastName, @CandidateInfoId OUTPUT
	SELECT @CandidateInfoId
END