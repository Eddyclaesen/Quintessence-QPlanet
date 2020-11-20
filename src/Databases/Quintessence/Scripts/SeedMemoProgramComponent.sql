DECLARE @id UNIQUEIDENTIFIER
DECLARE @simulationCombinationId UNIQUEIDENTIFIER

SET @simulationCombinationId = '42E38283-1DB5-405F-94BD-FEEB5D0E1BDF'

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, @simulationCombinationId, 1 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '1'
FROM dbo.Language

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, @simulationCombinationId, 2 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '2'
FROM dbo.Language


SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, @simulationCombinationId, 3 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '3'
FROM dbo.Language

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, @simulationCombinationId, 4 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '4'
FROM dbo.Language

SET @id = NEWID()
INSERT INTO dbo.SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (@Id, @simulationCombinationId, 5 )
INSERT INTO dbo.SimulationCombinationMemoTranslations (Id, SimulationCombinationMemoId, LanguageId, Title) 
SELECT 
	NEWID(), 
	@Id, 
	Id,
	Name + '5'
FROM dbo.Language






