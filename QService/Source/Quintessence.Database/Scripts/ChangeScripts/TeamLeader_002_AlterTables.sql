
-- CrmReplicationJobHistory
ALTER TABLE dbo.CrmReplicationJobHistory
ADD Info NVARCHAR(MAX) NULL
GO

-- CrmReplicationAssociate
ALTER TABLE dbo.CrmReplicationAssociate
ADD TeamLeaderName NVARCHAR(MAX) NULL,
	TeamLeaderId INT NULL,
	LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationAssociate
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationAssociate
ALTER COLUMN SyncVersion INT NOT NULL
GO


UPDATE dbo.CrmReplicationAssociate
SET TeamLeaderName = FirstName + ' ' + LastName
GO

-- CrmReplicationEmailAssociate
ALTER TABLE dbo.CrmReplicationEmailAssociate
ADD LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationEmailAssociate
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationEmailAssociate
ALTER COLUMN SyncVersion INT NOT NULL
GO

-- CrmReplicationProject
ALTER TABLE CrmReplicationProject
ADD TeamLeaderId INT NULL,
    LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationProject
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationProject
ALTER COLUMN SyncVersion INT NOT NULL
GO

-- CrmReplicationContact
ALTER TABLE CrmReplicationContact
ADD TeamLeaderId INT NULL,
    LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationContact
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationContact
ALTER COLUMN SyncVersion INT NOT NULL
GO

-- CrmReplicationEmail
ALTER TABLE dbo.CrmReplicationEmail
ADD LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationEmail
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationEmail
ALTER COLUMN SyncVersion INT NOT NULL
GO

-- CrmReplicationPerson
ALTER TABLE dbo.CrmReplicationPerson
ADD TeamLeaderId INT NULL,
    LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationPerson
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationPerson
ALTER COLUMN SyncVersion INT NOT NULL
GO


-- CrmReplicationAppointmentTraining
ALTER TABLE dbo.CrmReplicationAppointmentTraining
ADD LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationAppointmentTraining
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationAppointmentTraining
ALTER COLUMN SyncVersion INT NOT NULL
GO


-- CrmReplicationAppointmentTimesheet
ALTER TABLE dbo.CrmReplicationAppointmentTimesheet
ADD TeamLeaderId INT NULL,
	LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationAppointmentTimesheet
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationAppointmentTimesheet
ALTER COLUMN SyncVersion INT NOT NULL
GO

-- CrmReplicationAppointment
ALTER TABLE dbo.CrmReplicationAppointment
ADD TeamLeaderId INT NULL,
	LastSyncedUtc DATETIME NULL,
	SyncVersion INT NULL
GO

UPDATE dbo.CrmReplicationAppointment
SET SyncVersion = 1
GO

ALTER TABLE dbo.CrmReplicationAppointment
ALTER COLUMN SyncVersion INT NOT NULL
GO

