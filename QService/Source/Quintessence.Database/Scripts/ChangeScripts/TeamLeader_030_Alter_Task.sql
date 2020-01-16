USE [TeamLeader]
GO

ALTER TABLE Task
ADD IsFinished BIT NULL
GO

UPDATE Task
SET IsFinished = 0
GO

ALTER TABLE Task
ALTER COLUMN IsFinished BIT NOT NULL
GO
