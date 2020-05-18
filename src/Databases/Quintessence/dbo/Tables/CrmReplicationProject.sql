CREATE TABLE [dbo].[CrmReplicationProject] (
    [Id]              INT            NOT NULL,
    [Name]            NVARCHAR (MAX) NULL,
    [AssociateId]     INT            NULL,
    [ContactId]       INT            NULL,
    [ProjectStatusId] INT            NULL,
    [StartDate]       DATETIME       NULL,
    [BookyearFrom]    DATETIME       NULL,
    [BookyearTo]      DATETIME       NULL,
    [TeamLeaderId]    INT            NULL,
    [LastSyncedUtc]   DATETIME       NULL,
    [SyncVersion]     INT            NOT NULL,
    [SuperOfficeId]   INT            NULL,
    CONSTRAINT [PK_CrmReplicationProject] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO

CREATE TRIGGER [dbo].[History_CrmReplicationProject] ON [dbo].[CrmReplicationProject] FOR INSERT, UPDATE AS
BEGIN

BEGIN TRY

--declare @Name NVARCHAR(MAX)
--set @Name = (select Name from inserted i)

IF (SELECT Name FROM inserted i) LIKE '%-2018-%' OR (SELECT Name FROM inserted i) LIKE '%-18-%' BEGIN
	UPDATE dbo.CrmReplicationProject
	SET StartDate = '2018-01-01', BookyearFrom = '2018-01-01', BookyearTo = '2018-12-31 23:59:59.000'
	WHERE Id = (SELECT Id FROM inserted)
	--and StartDate is null
	END

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
	  SuperOfficeId,
          LastSyncedUtc ,
          SyncVersion
        )
SELECT d.Id,  d.Name,  d.AssociateId,  d.ContactId,  d.ProjectStatusId,  d.StartDate,  d.BookyearFrom,  d.BookyearTo,  d.TeamLeaderId, d.SuperOfficeId, d.LastSyncedUtc,  d.SyncVersion
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

