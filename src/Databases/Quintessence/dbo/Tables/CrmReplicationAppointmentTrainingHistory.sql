CREATE TABLE [dbo].[CrmReplicationAppointmentTrainingHistory] (
    [CrmReplicationAppointmentTrainingHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                                         INT            NOT NULL,
    [ProjectId]                                  INT            NULL,
    [AssociateId]                                INT            NULL,
    [OfficeId]                                   INT            NULL,
    [LanguageId]                                 INT            NULL,
    [StartDate]                                  DATETIME       NULL,
    [EndDate]                                    DATETIME       NULL,
    [Code]                                       NVARCHAR (12)  NULL,
    [Description]                                NVARCHAR (MAX) NULL,
    [LastSyncedUtc]                              DATETIME       NULL,
    [SyncVersion]                                INT            NOT NULL,
    CONSTRAINT [PK_CrmReplicationAppointmentTrainingHistory] PRIMARY KEY CLUSTERED ([CrmReplicationAppointmentTrainingHistoryId] ASC) WITH (FILLFACTOR = 90)
);

