CREATE TABLE [dbo].[CrmReplicationSuperOfficeEvent] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [EventType]     NVARCHAR (MAX) NULL,
    [ObjectType]    NVARCHAR (MAX) NULL,
    [ObjectId]      NVARCHAR (MAX) NULL,
    [ObjectChanges] NVARCHAR (MAX) NULL,
    [ByAssociateId] INT            NULL,
    [Source]        NVARCHAR (10)  NOT NULL,
    [ReceivedUtc]   DATETIME       NOT NULL,
    [ProcessCount]  INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationSuperOfficeEvent] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);


GO

----------------------------------------------------------------------------------------------------------------------------

CREATE TRIGGER [dbo].[History_CrmReplicationSuperOfficeEvent] ON [dbo].[CrmReplicationSuperOfficeEvent] FOR DELETE AS
BEGIN

BEGIN TRY

INSERT INTO dbo.CrmReplicationSuperOfficeEventHistory
        ( Id ,
          EventType ,
          ObjectType ,
          ObjectId ,
		  ObjectChanges,
		  ByAssociateId,
		  Source ,
          ReceivedUtc,
		  ProcessCount
        )
SELECT d.Id, d.EventType, d.ObjectType, d.ObjectId, d.ObjectChanges, d.ByAssociateId, d.Source, d.ReceivedUtc, d.ProcessCount FROM Deleted d

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

