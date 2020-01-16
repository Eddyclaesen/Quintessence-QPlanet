USE [Quintessence]
GO

-- Old Replication Job runs
DELETE FROM dbo.Job
WHERE JobDefinitionId IN
(
  SELECT Id 
  FROM dbo.JobDefinition
  WHERE [Assembly] = 'Quintessence.QJobService.JobDefinitions.Replication.dll'
) 
GO

-- Old Replication Job schedules
DELETE FROM dbo.JobSchedule
WHERE JobDefinitionId IN
(
  SELECT Id 
  FROM dbo.JobDefinition
  WHERE [Assembly] = 'Quintessence.QJobService.JobDefinitions.Replication.dll'
) 
GO

-- Old Replication JobDefinition
DELETE FROM dbo.JobDefinition
WHERE [Assembly] = 'Quintessence.QJobService.JobDefinitions.Replication.dll'
GO

--SELECT * FROM dbo.JobDefinition
--SELECT * FROM dbo.JobSchedule