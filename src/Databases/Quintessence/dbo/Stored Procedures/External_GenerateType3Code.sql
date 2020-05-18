CREATE PROCEDURE [dbo].[External_GenerateType3Code]
	@CrmParticipantId			INT,
	@FirstName					NVARCHAR(MAX),
	@LastName					NVARCHAR(MAX),
	@LanguageCode				NVARCHAR(2),
	@CategoryCode				NVARCHAR(20)
AS
BEGIN

	DECLARE @Password NVARCHAR(25)
	EXECUTE [Superoffice7].[dbo].[QPlanet_GenerateType3Code] @CrmParticipantId, 
																						@FirstName, 
																						@LastName, 
																						@LanguageCode,
																						@CategoryCode,
																						@Password OUTPUT
	SELECT @Password
END