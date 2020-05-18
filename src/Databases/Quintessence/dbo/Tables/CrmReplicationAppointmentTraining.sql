CREATE TABLE [dbo].[CrmReplicationAppointmentTraining] (
    [Id]            INT            NOT NULL,
    [ProjectId]     INT            NULL,
    [AssociateId]   INT            NULL,
    [OfficeId]      INT            NULL,
    [LanguageId]    INT            NULL,
    [StartDate]     DATETIME       NULL,
    [EndDate]       DATETIME       NULL,
    [Code]          NVARCHAR (12)  NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [LastSyncedUtc] DATETIME       NULL,
    [SyncVersion]   INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationAppointmentTraining] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


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
