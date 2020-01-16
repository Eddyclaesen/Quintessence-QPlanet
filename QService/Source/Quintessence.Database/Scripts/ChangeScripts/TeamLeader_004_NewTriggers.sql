
--CrmReplicationProject

CREATE TRIGGER [dbo].[History_CrmReplicationProject] ON [dbo].[CrmReplicationProject] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO dbo.CrmReplicationProjectHistory
        ( Id ,
          Name ,
          AssociateId ,
          ContactId ,
          ProjectStatusId ,
          StartDate ,
          BookyearFrom ,
          BookyearTo ,
          TeamLeaderId ,
          LastSyncedUtc ,
          SyncVersion
        )
SELECT d.Id,  d.Name,  d.AssociateId,  d.ContactId,  d.ProjectStatusId,  d.StartDate,  d.BookyearFrom,  d.BookyearTo,  d.TeamLeaderId,  d.LastSyncedUtc,  d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
--CrmReplicationEmail

CREATE TRIGGER [dbo].[History_CrmReplicationEmail] ON [dbo].[CrmReplicationEmail] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationEmailHistory]
           ([Id]
           ,[PersonId]
           ,[ContactId]
           ,[ContactName]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[DirectPhone]
           ,[MobilePhone]
           ,[LastSyncedUtc]
           ,[SyncVersion])
SELECT d.Id,  d.PersonId, d.ContactId, d.ContactName, d.FirstName, d.LastName, d.Email, d.DirectPhone, d.MobilePhone, d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationPerson

CREATE TRIGGER [dbo].[History_CrmReplicationPerson] ON [dbo].[CrmReplicationPerson] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO dbo.CrmReplicationPersonHistory
        ( Id ,
          ContactId ,
          FirstName ,
          LastName ,
          Title ,
          IsRetired ,
		  TeamLeaderID,
          LastSyncedUtc ,
          SyncVersion
        )
SELECT d.Id,  d.ContactId, d.FirstName, d.LastName, d.Title, d.IsRetired, d.TeamLeaderId, d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationEmailAssociate

CREATE TRIGGER [dbo].[History_CrmReplicationEmailAssociate] ON [dbo].[CrmReplicationEmailAssociate] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO dbo.CrmReplicationEmailAssociateHistory
        ( Id ,
          AssociateId ,
          Email ,
          [Rank] ,
          LastSyncedUtc ,
          SyncVersion
        )
SELECT d.Id,  d.AssociateId, d.Email, d.[Rank], d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationContact

CREATE TRIGGER [dbo].[History_CrmReplicationContact] ON [dbo].[CrmReplicationContact] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationContactHistory]
           ([Id]
           ,[Name]
           ,[Department]
           ,[AssociateId]
           ,[AccountManagerId]
           ,[CustomerAssistantId]
           ,[TeamLeaderId]
           ,[LastSyncedUtc]
           ,[SyncVersion]
		   )
SELECT d.Id,  d.Name, d.Department, d.AssociateId, d.AccountManagerId, d.CustomerAssistantId, d.TeamLeaderId, d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d 

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO


----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationAssociate

CREATE TRIGGER [dbo].[History_CrmReplicationAssociate] ON [dbo].[CrmReplicationAssociate] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationAssociateHistory]
        ( [Id] ,
          [UserName] ,
          [FirstName] ,
          [LastName] ,
          [UserGroupId] ,
          [TeamLeaderName] ,
          [TeamLeaderId] ,
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id,  d.UserName, d.FirstName, d.LastName, d.UserGroupId, d.TeamLeaderName, d.TeamLeaderId, d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationAppointmentTraining

CREATE TRIGGER [dbo].[History_CrmReplicationAppointmentTraining] ON [dbo].[CrmReplicationAppointmentTraining] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationAppointmentTrainingHistory]
        ( [Id] ,
          [ProjectId] ,
          [AssociateId] ,
          [OfficeId] ,
          [LanguageId] ,
          [StartDate] ,
          [EndDate] ,
          [Code] ,
          [Description] ,
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id,  d.ProjectId, d.AssociateId, d.OfficeId, d.LanguageId, d.StartDate, d.EndDate, d.Code, d.[Description], d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationAppointmentTimesheet

CREATE TRIGGER [dbo].[History_CrmReplicationAppointmentTimesheet] ON [dbo].[CrmReplicationAppointmentTimesheet] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationAppointmentTimesheetHistory]
        ( [Id] ,
          [ProjectId] ,
          [AssociateId] ,
          [StartDate] ,
          [EndDate] ,
          [Description] ,
          [TaskDescription] ,
		  [TeamLeaderId] ,
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id,  d.ProjectId, d.AssociateId, d.StartDate, d.EndDate, d.Description, d.TaskDescription, d.TeamLeaderId,  d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
-- CrmReplicationAppointment

CREATE TRIGGER [dbo].[History_CrmReplicationAppointment] ON [dbo].[CrmReplicationAppointment] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

INSERT INTO [dbo].[CrmReplicationAppointmentHistory]
        ( [Id] ,
          [AppointmentDate] ,
          [EndDate] ,
          [AssociateId] ,
          [IsReserved] ,
          [OfficeId] ,
          [LanguageId] ,
          [Gender] ,
          [Code] ,
          [FirstName] ,
          [LastName] ,
          [CrmProjectId] ,
          [TaskId] ,
          [Description] ,
		  [TeamLeaderId],
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id, d.AppointmentDate, d.EndDate, d.AssociateId, d.IsReserved, d.OfficeId, d.LanguageId, d.Gender, d.Code, d.FirstName, d.LastName, d.CrmProjectId, d.TaskId, 'n.a.', d.TeamLeaderId, d.LastSyncedUtc, d.SyncVersion
  FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO

----------------------------------------------------------------------------------------------------------------------------
--CrmReplicationTeamLeaderEvent

CREATE TRIGGER [dbo].[History_CrmReplicationTeamLeaderEvent] ON [dbo].[CrmReplicationTeamLeaderEvent] FOR DELETE AS
BEGIN

BEGIN TRY

INSERT INTO dbo.CrmReplicationTeamLeaderEventHistory
        ( Id ,
          EventType ,
          ObjectType ,
          ObjectId ,
		  Source ,
          ReceivedUtc,
		  ProcessCount
        )
SELECT d.Id, d.EventType, d.ObjectType, d.ObjectId, d.Source, d.ReceivedUtc, d.ProcessCount FROM Deleted d

END TRY

BEGIN CATCH

	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE(),
		   @ErrorSeverity = ERROR_SEVERITY(),
		   @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END CATCH
END
GO




