INSERT INTO Quintessence_QPlanet.QCandidate.MemoProgramComponents (Id, UserId, SimulationCombinationId, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn)
VALUES (newid(), CONVERT(uniqueidentifier,'7E6A3147-E23F-487D-AD4E-8608C199EF07'), CONVERT(uniqueidentifier, 'AAE0F86A-174E-4B31-81F5-B4A91DD55C77') , 'script', '2020-11-10 11:21:35.000', null, null)

INSERT INTO Quintessence_QPlanet.QCandidate.CalendarDays (Id, MemoProgramComponentId, Day, Note, CreatedBy, CreatedOn)
values(newid(), CONVERT(uniqueidentifier, '1DDA8169-E8BD-490C-8990-77703DB22654'), '2020-11-10', 'script', 'script', '2020-11-10')

INSERT INTO Quintessence_QPlanet.QCandidate.Memos(Id, MemoProgramComponentId, Position, OriginId, CreatedBy, CreatedOn)
values(newid(), CONVERT(uniqueidentifier, '1DDA8169-E8BD-490C-8990-77703DB22654'), 1, CONVERT(uniqueidentifier, 'FAF08EAC-BB2E-4771-AA42-6F42D9D26A10'), 'script', '2020-11-10')
INSERT INTO Quintessence_QPlanet.QCandidate.Memos(Id, MemoProgramComponentId, Position, OriginId, CreatedBy, CreatedOn)
values(newid(), CONVERT(uniqueidentifier, '1DDA8169-E8BD-490C-8990-77703DB22654'), 2, CONVERT(uniqueidentifier, 'F7BA8EF8-F5E5-4537-A9AA-FC25DBFDC2F4'), 'script', '2020-11-10')
INSERT INTO Quintessence_QPlanet.QCandidate.Memos(Id, MemoProgramComponentId, Position, OriginId, CreatedBy, CreatedOn)
values(newid(), CONVERT(uniqueidentifier, '1DDA8169-E8BD-490C-8990-77703DB22654'), 3, CONVERT(uniqueidentifier, 'BC8133CD-A8FE-4DFB-8B3E-4D410A7945B2'), 'script', '2020-11-10')
INSERT INTO Quintessence_QPlanet.QCandidate.Memos(Id, MemoProgramComponentId, Position, OriginId, CreatedBy, CreatedOn)
values(newid(), CONVERT(uniqueidentifier, '1DDA8169-E8BD-490C-8990-77703DB22654'), 4, CONVERT(uniqueidentifier, 'AAFAF9F0-1468-47E5-B50A-DD85A8234E52'), 'script', '2020-11-10')
INSERT INTO Quintessence_QPlanet.QCandidate.Memos(Id, MemoProgramComponentId, Position, OriginId, CreatedBy, CreatedOn)
values(newid(), CONVERT(uniqueidentifier, '1DDA8169-E8BD-490C-8990-77703DB22654'), 5, CONVERT(uniqueidentifier, 'FE4E6A00-1FA0-42D4-9B51-D65FB07E0AC9'), 'script', '2020-11-10')


insert into SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (newid(), CONVERT(uniqueidentifier, 'AAE0F86A-174E-4B31-81F5-B4A91DD55C77'), 1 )
insert into SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (newid(), CONVERT(uniqueidentifier, 'AAE0F86A-174E-4B31-81F5-B4A91DD55C77'), 2 )
insert into SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (newid(), CONVERT(uniqueidentifier, 'AAE0F86A-174E-4B31-81F5-B4A91DD55C77'), 3 )
insert into SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (newid(), CONVERT(uniqueidentifier, 'AAE0F86A-174E-4B31-81F5-B4A91DD55C77'), 4 )
insert into SimulationCombinationMemos (Id, SimulationCombinationId, Position) values (newid(), CONVERT(uniqueidentifier, 'AAE0F86A-174E-4B31-81F5-B4A91DD55C77'), 5 )


INSERT INTO [QCandidate].[MemoProgramComponents]
           ([Id]
           ,[UserId]
           ,[SimulationCombinationId]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           ( convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB')
           ,'7E6A3147-E23F-487D-AD4E-8608C199EF07'
           ,'AAE0F86A-174E-4B31-81F5-B4A91DD55C77'
           ,'script'
           ,SYSDATETIME())
GO


INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 1, convert(uniqueidentifier, '35CD4B69-CBAC-4009-8FF9-51B09FD3C66B'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 2, convert(uniqueidentifier, '3FDB4E45-E8FC-48D4-9CAA-1D7EE642F900'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 3, convert(uniqueidentifier, '36C4424E-6469-4407-B333-ED8B5A94D123'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 4, convert(uniqueidentifier, '45FC5C18-C417-4604-9C30-5969BE516A8B'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 5, convert(uniqueidentifier, 'EE2F1BF4-33A3-42EA-B5D5-7D11CCF1202A'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 6, convert(uniqueidentifier, '0507906F-6F74-4943-B06C-AE07FD09FCE9'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 7, convert(uniqueidentifier, 'A17F1473-429C-4240-91B4-20DA4D846443'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 8, convert(uniqueidentifier, 'CDCA44C7-8C3D-41DD-8263-BF49DB07AB58'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 9, convert(uniqueidentifier, 'B44582D6-B1B9-49F0-B30E-85B3331E8FC6'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 10, convert(uniqueidentifier, '67AF2803-1B83-47E0-961A-B7EEB5D105F2'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 11, convert(uniqueidentifier, 'C5BD45BB-2B64-4646-9CC7-96AD1EEAFC2E'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 12, convert(uniqueidentifier, 'FA657949-2B03-416E-ACE8-414C840FF2CC'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 13, convert(uniqueidentifier, 'F9BD00DC-8145-403D-963F-49E6E6E84E04'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 14, convert(uniqueidentifier, '3EF0F5B6-3F45-4C74-BF6F-699DBB3E0B95'), 'script', SYSDATETIME(), null, null)
INSERT INTO [QCandidate].[Memos] ([Id],[MemoProgramComponentId],[Position],[OriginId],[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]) values(newid(), convert(uniqueidentifier, 'DED8D11A-B0F4-42F7-947E-B9EEE2013EDB'), 15, convert(uniqueidentifier, '966FAF67-E0F7-462B-B27B-C3347FBEFB39'), 'script', SYSDATETIME(), null, null)
