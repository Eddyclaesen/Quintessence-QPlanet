CREATE FUNCTION [dbo].[Language_RetrieveLanguageId]
(
	@Code NVARCHAR(2)
)
RETURNS INT
AS
BEGIN
	DECLARE	@LanguageId INT

	SELECT	@LanguageId = Id 
	FROM	[dbo].[Language] 
	WHERE	Code = UPPER(LTRIM(RTRIM(@Code)))

	RETURN	ISNULL(@LanguageId, 1)
END