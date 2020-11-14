CREATE PROCEDURE [QCandidate].[MemoProgramComponent_GetByIdAndLanguage]
	@id UNIQUEIDENTIFIER,
    @languageId INT
AS

SET NOCOUNT ON;

SELECT
       MPC.[Id],
       MPC.[SimulationCombinationId],
       ST.[Name],
       PC.[Start],
       --Memo
       M.[Id], 
       M.[Position], 
       SCM.Position AS [OriginPosition], 
       SCMT.Title AS [Title],
       --CalendarDay
       CD.[Id], 
       CD.[Day], 
       CD.[Note]
FROM [QCandidate].[MemoProgramComponents] MPC WITH (NOLOCK)
    INNER JOIN dbo.[SimulationCombination] SC WITH (NOLOCK)
        ON SC.[Id] = MPC.[SimulationCombinationId]
    INNER JOIN dbo.[SimulationTranslation] ST WITH (NOLOCK)
			ON ST.[SimulationId] = SC.[SimulationId]
                AND ST.[LanguageId] = @languageId
    INNER JOIN dbo.[ProgramComponent] PC WITH (NOLOCK)
        ON PC.[Id] = MPC.[Id]
    INNER JOIN QCandidate.Memos M WITH (NOLOCK)
        ON M.[MemoProgramComponentId] = MPC.[Id]
	INNER JOIN [dbo].[SimulationCombinationMemos] SCM WITH (NOLOCK)
		ON SCM.[Id] = M.[OriginId]
    INNER JOIN [dbo].[SimulationCombinationMemoTranslations] SCMT WITH (NOLOCK)
        ON SCMT.[SimulationCombinationMemoId] = M.[OriginId]
            AND SCMT.[LanguageId] = @languageId
    INNER JOIN [QCandidate].[CalendarDays] CD WITH (NOLOCK)
        ON CD.[MemoProgramComponentId] =  MPC.[Id]
WHERE MPC.Id = @id