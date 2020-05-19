CREATE TABLE [dbo].[CrmReplicationAppointment] (
    [Id]              INT            NOT NULL,
    [AppointmentDate] DATETIME       NULL,
    [EndDate]         DATETIME       NULL,
    [AssociateId]     INT            NULL,
    [IsReserved]      BIT            NULL,
    [OfficeId]        INT            NULL,
    [LanguageId]      INT            NULL,
    [Gender]          VARCHAR (1)    NULL,
    [Code]            VARCHAR (12)   NULL,
    [FirstName]       NVARCHAR (MAX) NULL,
    [LastName]        NVARCHAR (MAX) NULL,
    [CrmProjectId]    INT            NULL,
    [TaskId]          INT            NULL,
    [Description]     TEXT           NULL,
    [TeamLeaderId]    INT            NULL,
    [LastSyncedUtc]   DATETIME       NULL,
    [SyncVersion]     INT            NOT NULL,
    [SuperOfficeId]   INT            NULL,
    CONSTRAINT [PK_CrmReplicationAppointment] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO

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
	  [SuperOfficeId],
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id, d.AppointmentDate, d.EndDate, d.AssociateId, d.IsReserved, d.OfficeId, d.LanguageId, d.Gender, d.Code, d.FirstName, d.LastName, d.CrmProjectId, d.TaskId, 'n.a.', d.TeamLeaderId, d.SuperOfficeId, d.LastSyncedUtc, d.SyncVersion
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

