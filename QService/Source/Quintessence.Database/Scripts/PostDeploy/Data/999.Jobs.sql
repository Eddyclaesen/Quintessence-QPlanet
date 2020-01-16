DECLARE @AppointmentReplicationJobDefinitionId AS UNIQUEIDENTIFIER = '5DA71B2E-5C91-48CE-990F-3E84A4A38CF0'
DECLARE @AppointmentTimesheetReplicationJobDefinitionId AS UNIQUEIDENTIFIER = 'BDED15C8-1241-482D-808F-A6F109A8E1D7'
DECLARE @AppointmentTrainingReplicationJobDefinitionId AS UNIQUEIDENTIFIER = '7F662140-31FD-46EF-898D-23BD454968CB'
DECLARE @AssociateReplicationJobDefinitionId AS UNIQUEIDENTIFIER = 'E3F27DE0-3A98-48A4-97FF-AF9ED06A0741'
DECLARE @ContactReplicationJobDefinitionId AS UNIQUEIDENTIFIER = 'B806D8B5-3590-4A45-9AC4-62CF68F66AD3'
DECLARE @EmailAssociateReplicationJobDefinitionId AS UNIQUEIDENTIFIER = 'E05006D0-7281-4C96-A466-DA3274CE26CC'
DECLARE @EmailReplicationJobDefinitionId AS UNIQUEIDENTIFIER = 'CC81DB9C-5198-41E7-8151-EF0E2AF05FB2'
DECLARE @ProjectReplicationJobDefinitionId AS UNIQUEIDENTIFIER = '5841ADD0-9EE9-433E-AAAE-87C9A0B991EB'
DECLARE @PersonReplicationJobDefinitionId AS UNIQUEIDENTIFIER = 'AF3C1D58-FB39-4A5E-B04E-27AC58001F8D'
DECLARE @ValidateInvoiceStatusJobDefinitionId AS UNIQUEIDENTIFIER = 'BEF7400B-03BA-4710-A28B-BA299E2F77C4'

INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@AppointmentReplicationJobDefinitionId, 'AppointmentReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.AppointmentReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@AppointmentTimesheetReplicationJobDefinitionId, 'AppointmentTimesheetReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.AppointmentTimesheetReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@AppointmentTrainingReplicationJobDefinitionId, 'AppointmentTrainingReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.AppointmentTrainingReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@AssociateReplicationJobDefinitionId, 'AssociateReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.AssociateReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@ContactReplicationJobDefinitionId, 'ContactReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.ContactReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@EmailAssociateReplicationJobDefinitionId, 'EmailAssociateReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.EmailAssociateReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@EmailReplicationJobDefinitionId, 'EmailReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.EmailReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@ProjectReplicationJobDefinitionId, 'ProjectReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.ProjectReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@PersonReplicationJobDefinitionId, 'PersonReplication', 'Quintessence.QJobService.JobDefinitions.Replication.dll', 'Quintessence.QJobService.JobDefinitions.Replication.PersonReplication', 1)
INSERT INTO [JobDefinition]([Id], [Name], [Assembly], [Class], [IsEnabled]) VALUES (@ValidateInvoiceStatusJobDefinitionId, 'ValidateInvoiceStatus', 'Quintessence.QJobService.JobDefinitions.Invoicing.dll', 'Quintessence.QJobService.JobDefinitions.Invoicing.ValidateInvoiceStatus', 1)

INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @AppointmentReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 60, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @AppointmentTimesheetReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 60, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @AppointmentTrainingReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 60, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @AssociateReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 3600, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @ContactReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 3600, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @EmailAssociateReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 3600, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @EmailReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 3600, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @ProjectReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 3600, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @PersonReplicationJobDefinitionId, '00:00:00.000', '23:59:59.000', 3600, 1)
INSERT INTO [JobSchedule]([Id], [JobDefinitionId], [StartTime], [EndTime], [Interval], [IsEnabled]) VALUES (NEWID(), @ValidateInvoiceStatusJobDefinitionId, '01:00:00.000', '01:01:00.000', 120, 1)