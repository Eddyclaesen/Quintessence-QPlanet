CREATE PROCEDURE [dbo].[SimulationCombinationMemo_GetAllBySimulationCombinationId]
	@simulationCombinationId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON

SELECT
	[Id],
	[SimulationCombinationId],
	[Position]
FROM [dbo].[SimulationCombinationMemos]
WHERE [SimulationCombinationId] = @simulationCombinationId
