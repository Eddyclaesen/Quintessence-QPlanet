CREATE TABLE [dbo].[CrmReplicationAppointmentTimesheet] (
    [Id]              INT            NOT NULL,
    [ProjectId]       INT            NULL,
    [AssociateId]     INT            NULL,
    [StartDate]       DATETIME2 (7)  NULL,
    [EndDate]         DATETIME2 (7)  NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [TaskDescription] NVARCHAR (150) NULL,
    [TeamLeaderId]    INT            NULL,
    [LastSyncedUtc]   DATETIME       NULL,
    [SyncVersion]     INT            NOT NULL,
    [SuperOfficeId]   INT            NULL,
    CONSTRAINT [PK_CrmReplicationAppointmentTimesheet] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO

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
	  [SuperOfficeId],
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id,  d.ProjectId, d.AssociateId, d.StartDate, d.EndDate, d.Description, d.TaskDescription, d.TeamLeaderId, d.SuperOfficeId, d.LastSyncedUtc, d.SyncVersion
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

