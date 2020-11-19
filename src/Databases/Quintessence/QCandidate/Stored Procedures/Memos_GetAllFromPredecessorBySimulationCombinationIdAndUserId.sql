CREATE PROCEDURE [QCandidate].[Memos_GetAllFromPredecessorBySimulationCombinationIdAndUserId]
	@simulationCombinationId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON

SELECT 
	M.[Id], 
	M.[MemoProgramComponentId],
	M.[OriginId], 
	M.[Position]
FROM [QCandidate].[Memos] M
	INNER JOIN [QCandidate].[MemoProgramComponents] MPC
		ON MPC.[Id] = M.[MemoProgramComponentId]
			AND MPC.[UserId] = @userId
			AND CONVERT(DATE, MPC.[CreatedOn]) = CONVERT(DATE, GETUTCDATE())
	INNER JOIN [dbo].[SimulationCombination] SC
		ON SC.[PredecessorId] = MPC.[SimulationCombinationId]
WHERE SC.[Id] = @simulationCombinationId 
    AND MPC.[UserId] = @userId

