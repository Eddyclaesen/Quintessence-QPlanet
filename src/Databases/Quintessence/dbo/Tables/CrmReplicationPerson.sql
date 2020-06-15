CREATE TABLE [dbo].[CrmReplicationPerson] (
    [Id]            INT            NOT NULL,
    [ContactId]     INT            NULL,
    [FirstName]     NVARCHAR (MAX) NULL,
    [LastName]      NVARCHAR (MAX) NULL,
    [Title]         NVARCHAR (MAX) NULL,
    [IsRetired]     BIT            CONSTRAINT [DF_CrmReplicationPerson_IsRetired] DEFAULT ((0)) NULL,
    [TeamLeaderId]  INT            NULL,
    [LastSyncedUtc] DATETIME       NULL,
    [SyncVersion]   INT            NOT NULL,
    [SuperOfficeId] INT            NULL,
    CONSTRAINT [PK_CrmReplicationPerson] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO


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
	  TeamLeaderId,
	  SuperOfficeId,
          LastSyncedUtc ,
          SyncVersion
        )
SELECT d.Id,  d.ContactId, d.FirstName, d.LastName, d.Title, d.IsRetired, d.TeamLeaderId, d.SuperOfficeId, d.LastSyncedUtc, d.SyncVersion
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

