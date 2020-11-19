DECLARE @id UNIQUEIDENTIFIER

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5', 1 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '1'
FROM dbo.Language

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5', 2 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '2'
FROM dbo.Language


SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5', 3 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '3'
FROM dbo.Language

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5', 4 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '4'
FROM dbo.Language

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5', 5 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '5'
FROM dbo.Language


INSERT INTO [QCandidate].[MemoProgramComponents]
           ([Id]
           ,[UserId]
           ,[SimulationCombinationId]
           ,[CreatedBy]
           ,[CreatedOn]
			)
     VALUES (
		'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB',
        '7E6A3147-E23F-487D-AD4E-8608C199EF07',
        '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5',
        'script',
        GETDATE())


INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId], [Position], [OriginId],[CreatedBy],[CreatedOn])
SELECT
	NEWID(),
	'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB',
	Position,
	Id,
	'script',
	GETDATE()
FROM dbo.SimulationCombinationMemos
WHERE SimulationCombinationId = '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5'

DECLARE @date DATE

SET @date = '2018-04-02'
WHILE (@date <= '2018-04-30')
	BEGIN

	IF (DATEPART(dw, @date) < 6)
	BEGIN

	INSERT INTO QCandidate.CalendarDays (Id, MemoProgramComponentId, Day, CreatedBy, CreatedOn)
	VALUES (NEWID(), 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB', @date, 'script', GETDATE())

	END

	SET @date = DATEADD(dd, 1, @date)

	END



update [dbo].[SimulationCombination] set QCandidateLayoutId = 3 where id = '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5';


update [dbo].[SimulationCombination] set QCandidateLayoutId = 3 where id = '01FBB298-AE9A-4BFF-BD9A-A2750FF5A0B5';


