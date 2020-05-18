CREATE TABLE [dbo].[CrmReplicationEmail] (
    [Id]            INT            NOT NULL,
    [PersonId]      INT            NULL,
    [ContactId]     INT            NULL,
    [ContactName]   NVARCHAR (MAX) NULL,
    [FirstName]     NVARCHAR (MAX) NULL,
    [LastName]      NVARCHAR (MAX) NULL,
    [Email]         NVARCHAR (MAX) NULL,
    [DirectPhone]   NVARCHAR (MAX) NULL,
    [MobilePhone]   NVARCHAR (MAX) NULL,
    [LastSyncedUtc] DATETIME       NULL,
    [SyncVersion]   INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationEmail] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


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
