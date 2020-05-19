CREATE TABLE [dbo].[CrmReplicationSuperOfficeEventHistory] (
    [CrmReplicationSuperOfficeEventHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                                      INT            NOT NULL,
    [EventType]                               NVARCHAR (MAX) NULL,
    [ObjectType]                              NVARCHAR (MAX) NULL,
    [ObjectId]                                NVARCHAR (MAX) NULL,
    [ObjectChanges]                           NVARCHAR (MAX) NULL,
    [ByAssociateId]                           INT            NULL,
    [Source]                                  NVARCHAR (10)  NOT NULL,
    [ReceivedUtc]                             DATETIME       NOT NULL,
    [ProcessCount]                            INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationSuperOfficeEventHistory] PRIMARY KEY CLUSTERED ([CrmReplicationSuperOfficeEventHistoryId] ASC) WITH (FILLFACTOR = 90)
);

