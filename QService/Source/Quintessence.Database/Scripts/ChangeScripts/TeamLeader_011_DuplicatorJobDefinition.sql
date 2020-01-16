USE [Quintessence]
GO

INSERT INTO [dbo].[JobDefinition] ( [Id] , [Name] , [Assembly] , [Class] , [IsEnabled] )
VALUES  ( N'88141238-FB9B-4B1D-82F1-7108BB880D9A', -- Id - uniqueidentifier
          N'TeamLeaderDuplication' , -- Name - nvarchar(200)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.dll' , -- Assembly - nvarchar(max)
          N'Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeaderDuplication' , -- Class - nvarchar(max)
          1  -- IsEnabled - bit
        )
GO

INSERT INTO [dbo].[JobSchedule] ([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled])
     VALUES
           ('443D0DF2-BE07-4432-A841-3430F09CA620'
           ,'88141238-FB9B-4B1D-82F1-7108BB880D9A'
           ,'02:00:00.0000000'
           ,'04:59:59.0000000'
           ,36000
           ,1)
GO

-- For Debugging, change schedule:
--UPDATE dbo.JobSchedule
--SET StartTime = '00:00:00.0000000', EndTime = '23:59:59.0000000'
--FROM dbo.JobSchedule
--WHERE JobDefinitionId = '88141238-FB9B-4B1D-82F1-7108BB880D9A'

--UPDATE dbo.JobSchedule
--SET StartTime = '02:00:00.0000000', EndTime = '04:59:59.0000000'
--FROM dbo.JobSchedule
--WHERE JobDefinitionId = '88141238-FB9B-4B1D-82F1-7108BB880D9A'
