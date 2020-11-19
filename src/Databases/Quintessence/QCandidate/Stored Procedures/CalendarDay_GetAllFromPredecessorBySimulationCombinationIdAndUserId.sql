CREATE PROCEDURE [QCandidate].[CalendarDay_GetAllFromPredecessorBySimulationCombinationIdAndUserId]
	@simulationCombinationId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER
AS

SET NOCOUNT ON

SELECT 
	[CD].[Id],
	[CD].[Note]
FROM [QCandidate].[CalendarDays] CD
    INNER JOIN [QCandidate].[MemoProgramComponents] MPC
		ON MPC.[Id] = CD.[MemoProgramComponentId]
			AND MPC.[UserId] = @userId
	INNER JOIN [dbo].[SimulationCombination] SC
		ON SC.[PredecessorId] = MPC.[SimulationCombinationId]
WHERE [SC].[Id] = @simulationCombinationId
	AND CONVERT(DATE, MPC.[CreatedOn]) = CONVERT(DATE, GETUTCDATE())

GO