CREATE PROCEDURE [QCandidate].[GetPredecessorCalendarDays_GetBySimulationCombinationIdAndUserId]
	@simulationCombinationId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON

SELECT [QCandidate].[CalendarDays].[Id],  [QCandidate].[CalendarDays].[Note]
FROM  [QCandidate].[CalendarDays]
    INNER JOIN [QCandidate].[MemoProgramComponents] 
		ON [QCandidate].[CalendarDays].[MemoProgramComponentId] = [QCandidate].[MemoProgramComponents].[Id]
	INNER JOIN [dbo].[SimulationCombination] 
		ON [QCandidate].[MemoProgramComponents].[SimulationCombinationId] = [dbo].[SimulationCombination].[PredecessorId]
WHERE [dbo].[SimulationCombination].[Id] = @simulationCombinationId
	AND [QCandidate].[MemoProgramComponents].[UserId] = @userId
