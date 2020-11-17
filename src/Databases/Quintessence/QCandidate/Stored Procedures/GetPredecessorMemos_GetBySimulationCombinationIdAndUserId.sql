CREATE PROCEDURE [QCandidate].[GetPredecessorMemos_GetBySimulationCombinationIdAndUserId]
	@simulationCombinationId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON

SELECT 
	[QCandidate].[Memos].[Id] , [QCandidate].[Memos].[MemoProgramComponentId], [QCandidate].[Memos].[OriginId], [QCandidate].[Memos].[Position]
FROM [QCandidate].[Memos]
	INNER JOIN [QCandidate].[MemoProgramComponents] 
		ON [QCandidate].[Memos].[MemoProgramComponentId] = [QCandidate].[MemoProgramComponents].[Id]
	INNER JOIN [dbo].[SimulationCombination] 
		ON [QCandidate].[MemoProgramComponents].[SimulationCombinationId] = [dbo].[SimulationCombination].[PredecessorId]
WHERE [dbo].[SimulationCombination].[Id] = @simulationCombinationId 
    AND [QCandidate].[MemoProgramComponents].[UserId] = @userId
    AND CONVERT(DATE, [QCandidate].[Memos].[CreatedOn]) = CONVERT(DATE, [QCandidate].[MemoProgramComponents].[CreatedOn])
