
-- CrmReplicationUserGroup
-- extra entry 'From TeamLeader', gebruikt als ID in crmReplicationAssociate.UserGroupID bij sync nieuwe user vanuit TeamLeader

INSERT INTO dbo.CrmReplicationUserGroup (Id, Name, Rank )
VALUES  ( 27, -- Id - int
          N'From TeamLeader', -- Name - nvarchar(max)
          27  -- Rank - int
          )


-- CrmReplicationSettings

INSERT INTO dbo.CrmReplicationSettings ( Id, [Key], Value )
VALUES  ( 1, -- Id - int
          N'TeamLeaderAPI_Group',
          N'16961'
          )
GO

INSERT INTO dbo.CrmReplicationSettings ( Id, [Key], Value )
VALUES  ( 2, -- Id - int
          N'TeamLeaderAPI_Key',
          N'S7CFlCOQeMxI7nfPmeXdQ7ZIdB7eDNtmKWRWpwCt8026e8MrczvSwkrALpEf2YrDK9pZvSjuIcrNa34cspVPzcfasKglyxO9x4ZSN9cmctZcnTfF90ly3qzl1Fwp3bpq6f9KecWgvMjJwoeveqU7GjjxPGNJH30E8Zed11knDtWZI1lMh5KOfUcUqQuXJkrI9wL1nWfQ'
          )
GO

INSERT INTO dbo.CrmReplicationSettings ( Id, [Key], Value )
VALUES  ( 3, -- Id - int
          N'TeamLeaderAPI_BaseUrl',
          N'https://www.teamleader.be/api'
          )
GO

INSERT INTO dbo.CrmReplicationSettings ( Id, [Key], Value )
VALUES  ( 4, -- Id - int
          N'TeamLeaderEventReplicator_EventBatchSize',
          N'10'
          )
GO

INSERT INTO dbo.CrmReplicationSettings ( Id, [Key], Value )
VALUES  ( 5, -- Id - int
          N'TeamLeaderEventReplicator_EventMaxProcessCount',
          N'2'
          )
GO

-- CrmReplicationProjectStatusMapping

INSERT INTO dbo.CrmReplicationProjectStatusMapping ( Id, SourceValue, TargetId )
VALUES  ( 1, -- Id - int
          N'active', -- SourceValue
          3  -- TargetId 'Lopend'
          )
GO

INSERT INTO dbo.CrmReplicationProjectStatusMapping ( Id, SourceValue, TargetId )
VALUES  ( 2, -- Id - int
          N'on hold', -- SourceValue
          3  -- TargetId 'Lopend'
          )
GO

INSERT INTO dbo.CrmReplicationProjectStatusMapping ( Id, SourceValue, TargetId )
VALUES  ( 3, -- Id - int
          N'cancelled', -- SourceValue
          4  -- TargetId 'Voltooid'
          )
GO

INSERT INTO dbo.CrmReplicationProjectStatusMapping ( Id, SourceValue, TargetId )
VALUES  ( 4, -- Id - int
          N'done', -- SourceValue
          4  -- TargetId 'Voltooid'
          )
GO
