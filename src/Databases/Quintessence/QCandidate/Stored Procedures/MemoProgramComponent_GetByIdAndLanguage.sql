CREATE PROCEDURE [QCandidate].[MemoProgramComponent_GetByIdAndLanguage]
	@id UNIQUEIDENTIFIER,
    @languageId INT
AS

SET NOCOUNT ON;

SELECT
       MPC.[Id],
       MPC.[SimulationCombinationId],
       --Memo
       M.[Id], 
       M.[Position], 
       SCM.Position AS [OriginPosition], 
       SCMT.Title AS [Title],
       --CalendarDay
       CD.[Id], 
       CD.[Day], 
       CD.[Note]
FROM [QCandidate].[MemoProgramComponents] MPC
    INNER JOIN QCandidate.Memos M
        ON M.[MemoProgramComponentId] = MPC.[Id]
	INNER JOIN [dbo].[SimulationCombinationMemos] SCM
		ON SCM.[Id] = M.[OriginId]
    INNER JOIN [dbo].[SimulationCombinationMemoTranslations] SCMT
        ON SCMT.[SimulationCombinationMemoId] = M.[OriginId]
            AND SCMT.[LanguageId] = @languageId
    INNER JOIN [QCandidate].[CalendarDays] CD
        ON CD.[MemoProgramComponentId] =  MPC.[Id]
WHERE MPC.Id = @id