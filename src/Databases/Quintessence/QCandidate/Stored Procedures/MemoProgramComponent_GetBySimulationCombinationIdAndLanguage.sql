CREATE PROCEDURE [QCandidate].[MemoProgramComponent_GetBySimulationCombinationIdAndLanguage]
	@id UNIQUEIDENTIFIER,
    @language char(2)
AS

	SET NOCOUNT ON;
	DECLARE @LanguageId int
	SET @LanguageId = case @language
		when 'nl' then 1
		when 'fr' then 2
		else 3
	end

SELECT
       --SimulationCombination
       MPC.Id AS [Id],
       --Memos
       MEM.Id AS [Id], MEM.Position AS [Position], MPC.Id AS [MemoProgramId], SCM.Id AS [OriginId], SCM.Position AS [OriginPosition], SCMT.Title AS [Title],
       --CalendarDays
       CA.Id, CA.Day, CA.Note
FROM
         SimulationCombination
    INNER JOIN dbo.SimulationCombinationMemos SCM
        ON SimulationCombination.id = SCM.SimulationCombinationId
    inner join QCandidate.MemoProgramComponents MPC
        on SimulationCombination.Id = MPC.SimulationCombinationId
    inner JOIN dbo.SimulationCombinationMemoTranslations SCMT
        ON SCM.Id = SCMT.SimulationCombinationMemoId
    inner JOIN QCandidate.Memos MEM
        ON SCM.Id = MEM.OriginId
    inner join QCandidate.CalendarDays CA on MPC.Id = CA.MemoProgramComponentId
WHERE
      MPC.SimulationCombinationId = @id AND SCMT.LanguageId = @LanguageId
go

