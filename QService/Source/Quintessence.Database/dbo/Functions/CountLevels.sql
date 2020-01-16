CREATE FUNCTION [dbo].[CountLevels]
(
	@ProjectCandidateId NVARCHAR(max),
	@DictionaryClusterId NVARCHAR(max),
	@DictionaryCompetenceId NVARCHAR(max)

)
RETURNS INT
AS
BEGIN
	DECLARE		@Counter INT
	
	SET @Counter = (
				SELECT		COUNT(distinct DictionaryLevelLevel)
	
				FROM		[dbo].[ProjectCandidateIndicatorSimulationScoreView] 
	
				WHERE		ProjectCandidateId = @ProjectCandidateId
						AND DictionaryClusterId = @DictionaryClusterId
						AND DictionaryCompetenceId = @DictionaryCompetenceId
					)
	RETURN @Counter
END