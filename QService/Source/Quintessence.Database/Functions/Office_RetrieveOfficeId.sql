CREATE FUNCTION [dbo].[Office_RetrieveOfficeId]
(
	@ShortName NVARCHAR(3)
)
RETURNS INT
AS
BEGIN
	DECLARE	@OfficeId INT

	IF LEN(@ShortName) = 3
	BEGIN
		IF SUBSTRING(@ShortName, 3, 1) = ';'
		BEGIN
			SELECT	@OfficeId = Id 
			FROM	[dbo].[Office] 
			WHERE	ShortName = UPPER(LTRIM(RTRIM(SUBSTRING(@ShortName, 1, 2))))

			RETURN @OfficeId
		END
		ELSE
		BEGIN
			RETURN 1 --'QA'
		END
	END
	ELSE
	BEGIN 
		IF LEN(@ShortName) = 2
		BEGIN
			SELECT	@OfficeId = Id 
			FROM	[dbo].[Office] 
			WHERE	ShortName = UPPER(LTRIM(RTRIM(SUBSTRING(@ShortName, 1, 2))))

			RETURN @OfficeId
		END
		ELSE
		BEGIN
			RETURN 1 --'QA'
		END
	END	

	RETURN 1
END
