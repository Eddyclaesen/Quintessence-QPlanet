CREATE FUNCTION [SimulationCombinationLanguage_ConcatenateLanguageNames]
(
	@SimulationCombinationId	UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @LanguageList VARCHAR(8000)

	SELECT		@LanguageList = COALESCE(@LanguageList + ', ', '') + ISNULL([LanguageName], 'N/A')
	FROM		[SimulationCombinationLanguageView]
	WHERE		[SimulationCombinationLanguageView].[SimulationCombinationId] = @SimulationCombinationId
	
	RETURN @LanguageList
END