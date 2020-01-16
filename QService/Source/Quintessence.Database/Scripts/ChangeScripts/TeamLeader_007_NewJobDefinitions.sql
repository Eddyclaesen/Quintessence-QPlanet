

-- ProjectIdReplication
-- DLL
--INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
--VALUES  ( NEWID() , -- Id - uniqueidentifier
--          N'TeamLeaderCrmReplicationProjectId' , -- Name - nvarchar(200)
--          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.dll' , -- Assembly - nvarchar(max)
--          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.ProjectIdReplication' , -- Class - nvarchar(max)
--          1  -- IsEnabled - bit
--        )
--GO

-- For CrmReplicationJob :
INSERT INTO dbo.CrmReplicationJob ( Id, Name, Description )
VALUES  ( N'12904395-EAD1-4F41-8250-9032D8270BE3', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationProjectId', -- Name - nvarchar(200)
          'Sync TeamLeader ProjectIds From TeamLeader to CrmReplicationProject'  -- Description - text
          )
GO

-----------------------------------------------------------------------------------------------------------------------------

-- AssociateReplication
-- DLL
INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
VALUES  ( N'DE27B24F-2556-4D6F-AE28-5C99B0A20D1F', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationAssociate' , -- Name - nvarchar(200)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.dll' , -- Assembly - nvarchar(max)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.AssociateReplication' , -- Class - nvarchar(max)
          1  -- IsEnabled - bit
        )
GO

INSERT INTO [dbo].[JobSchedule] ([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled])
     VALUES
           ('D1255CA6-3F06-4A44-BC58-5147B5CC168D'
           ,'DE27B24F-2556-4D6F-AE28-5C99B0A20D1F'
           ,'00:00:00.0000000'
           ,'23:59:59.0000000'
           ,3600
           ,1)
GO

-- For CrmReplicationJob :
INSERT INTO dbo.CrmReplicationJob ( Id, Name, Description )
VALUES  ( N'447B7BBE-5505-4F2B-95A5-C2F150248667', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationAssociate', -- Name - nvarchar(200)
          'Sync Associates From TeamLeader to CrmReplicationAssociates'  -- Description - text
          )
GO

-----------------------------------------------------------------------------------------------------------------------------

-- AssociateEmailReplication
-- DLL
INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
VALUES  ( N'312C10B8-657A-43BF-A1F3-5660A6F54209', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationAssociateEmail' , -- Name - nvarchar(200)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.dll' , -- Assembly - nvarchar(max)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.AssociateEmailReplication' , -- Class - nvarchar(max)
          1  -- IsEnabled - bit
        )
GO

INSERT INTO [dbo].[JobSchedule] ([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled])
     VALUES
           ('D353A4E4-1541-4135-8E3B-80DAB62172A8'
           ,'312C10B8-657A-43BF-A1F3-5660A6F54209'
           ,'00:00:00.0000000'
           ,'23:59:59.0000000'
           ,3600
           ,1)
GO

-- For CrmReplicationJob :
INSERT INTO dbo.CrmReplicationJob ( Id, Name, Description )
VALUES  ( N'94815067-249C-455C-989B-A975C8767AE5', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationAssociateEmail', -- Name - nvarchar(200)
          'Sync Associates Email From TeamLeader to CrmReplicationEmailAssociate'  -- Description - text
          )
GO

-----------------------------------------------------------------------------------------------------------------------------

-- ContactReplication
-- DLL
--INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
--VALUES  ( NEWID() , -- Id - uniqueidentifier
--          N'TeamLeaderCrmReplicationContactId' , -- Name - nvarchar(200)
--          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.dll' , -- Assembly - nvarchar(max)
--          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.ContactIdReplication' , -- Class - nvarchar(max)
--          1  -- IsEnabled - bit
--        )
--GO

-- For CrmReplicationJob :
INSERT INTO dbo.CrmReplicationJob ( Id, Name, Description )
VALUES  ( N'8FAFC043-6DAE-475A-A3C4-3753739DE5A5', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationContactId', -- Name - nvarchar(200)
          'Sync Contact Ids From TeamLeader to CrmReplicationContact'  -- Description - text
          )
GO

-----------------------------------------------------------------------------------------------------------------------------

-- PersonReplication
-- DLL
--INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
--VALUES  ( NEWID() , -- Id - uniqueidentifier
--          N'TeamLeaderCrmReplicationPersonId' , -- Name - nvarchar(200)
--          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.dll' , -- Assembly - nvarchar(max)
--          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.PersonIdReplication' , -- Class - nvarchar(max)
--          1  -- IsEnabled - bit
--        )
--GO

-- For CrmReplicationJob :
INSERT INTO dbo.CrmReplicationJob ( Id, Name, Description )
VALUES  ( N'4AD4D88E-D23D-4DEB-A640-862B0F056B66', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationPersonId', -- Name - nvarchar(200)
          'Sync Person Ids From TeamLeader to CrmReplicationPerson'  -- Description - text
          )
GO
-----------------------------------------------------------------------------------------------------------------------------

-- TeamLeaderEvents
-- DLL
INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
VALUES  ( N'759DD1B3-673E-4A4D-91B7-7DAB78DC86D5', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationTeamLeaderEvent' , -- Name - nvarchar(200)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.dll' , -- Assembly - nvarchar(max)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeaderEventReplication' , -- Class - nvarchar(max)
          1  -- IsEnabled - bit
        )
GO

INSERT INTO [dbo].[JobSchedule] ([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled])
     VALUES
           ('440E52FF-3688-4455-9A53-CA6DF8DE3F81'
           ,'759DD1B3-673E-4A4D-91B7-7DAB78DC86D5'
           ,'00:00:00.0000000'
           ,'23:59:59.0000000'
           ,30
           ,1)
GO

-- For CrmReplicationJob :
INSERT INTO dbo.CrmReplicationJob ( Id, Name, Description )
VALUES  ( N'7008EA7F-0957-4652-8498-4ADC8D4F6DCA', -- Id - uniqueidentifier
          N'TeamLeaderCrmReplicationTeamLeaderEvent', -- Name - nvarchar(200)
          'Sync Events From TeamLeader WebHook to CrmReplication tables'  -- Description - text
          )
GO
