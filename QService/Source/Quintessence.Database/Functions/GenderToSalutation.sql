CREATE FUNCTION [dbo].[GenderToSalutation]
(
	@Gender NVARCHAR(1),
	@LanguageId INT
)
RETURNS NVARCHAR(25)
AS
BEGIN
	DECLARE @Salutation NVARCHAR(25)
	IF @LanguageId = 1 --Dutch
	BEGIN
		IF @Gender = 'M'
		BEGIN
			SET @Salutation = 'Mijnheer'
		END
		ELSE
		BEGIN
			SET @Salutation = 'Mevrouw'
		END
	END
	ELSE IF @LanguageId = 2 --French
	BEGIN
		IF @Gender = 'M'
		BEGIN
			SET @Salutation = 'Monsieur'
		END
		ELSE
		BEGIN
			SET @Salutation = 'Madame'
		END
	END
	ELSE IF @LanguageId = 4 --German
	BEGIN
		IF @Gender = 'M'
		BEGIN
			SET @Salutation = 'Herr'
		END
		ELSE
		BEGIN
			SET @Salutation = 'Frau'
		END
	END
	ELSE --English (or other languages)
	BEGIN
		IF @Gender = 'M'
		BEGIN
			SET @Salutation = 'Mr.'
		END
		ELSE
		BEGIN
			SET @Salutation = 'Mrs.'
		END
	END

	RETURN @Salutation
END
