CREATE TABLE [dbo].[CrmReplicationTeamLeaderEvent] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [EventType]    NVARCHAR (MAX) NULL,
    [ObjectType]   NVARCHAR (MAX) NULL,
    [ObjectId]     NVARCHAR (MAX) NULL,
    [Source]       NVARCHAR (10)  NOT NULL,
    [ReceivedUtc]  DATETIME       NOT NULL,
    [ProcessCount] INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationTeamLeaderEvent] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


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
