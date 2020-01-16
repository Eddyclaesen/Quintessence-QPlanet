UPDATE dbo.JobDefinition
SET IsEnabled = 0
WHERE Name NOT LIKE 'TeamLeaderCrmReplication%'
GO

UPDATE dbo.JobSchedule
SET IsEnabled = 0 
FROM dbo.JobSchedule
  JOIN dbo.JobDefinition ON JobDefinition.Id = JobSchedule.JobDefinitionId
WHERE Name NOT LIKE 'TeamLeaderCrmReplication%'
GO

