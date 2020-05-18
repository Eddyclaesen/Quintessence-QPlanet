CREATE TABLE [dbo].[CrmReplicationEmailAssociate] (
    [Id]            INT            NOT NULL,
    [AssociateId]   INT            NULL,
    [Email]         NVARCHAR (MAX) NULL,
    [Rank]          INT            NULL,
    [LastSyncedUtc] DATETIME       NULL,
    [SyncVersion]   INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationEmailAssociate] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


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
