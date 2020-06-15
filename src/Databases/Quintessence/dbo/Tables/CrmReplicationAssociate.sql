CREATE TABLE [dbo].[CrmReplicationAssociate] (
    [Id]             INT            NOT NULL,
    [UserName]       NVARCHAR (MAX) NULL,
    [FirstName]      NVARCHAR (MAX) NULL,
    [LastName]       NVARCHAR (MAX) NULL,
    [UserGroupId]    INT            NULL,
    [TeamLeaderName] NVARCHAR (MAX) NULL,
    [TeamLeaderId]   INT            NULL,
    [LastSyncedUtc]  DATETIME       NULL,
    [SyncVersion]    INT            NOT NULL,
    [SuperOfficeId]  INT            NULL,
    CONSTRAINT [PK_CrmReplicationAssociate] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO

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
	  [SuperOfficeId],
          [LastSyncedUtc] ,
          [SyncVersion]
        )
SELECT d.Id,  d.UserName, d.FirstName, d.LastName, d.UserGroupId, d.TeamLeaderName, d.TeamLeaderId, d.SuperOfficeId, d.LastSyncedUtc, d.SyncVersion
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

