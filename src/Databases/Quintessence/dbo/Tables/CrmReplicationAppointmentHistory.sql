CREATE TABLE [dbo].[CrmReplicationAppointmentHistory] (
    [CrmReplicationAppointmentHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                                 INT            NOT NULL,
    [AppointmentDate]                    DATETIME       NULL,
    [EndDate]                            DATETIME       NULL,
    [AssociateId]                        INT            NULL,
    [IsReserved]                         BIT            NULL,
    [OfficeId]                           INT            NULL,
    [LanguageId]                         INT            NULL,
    [Gender]                             VARCHAR (1)    NULL,
    [Code]                               VARCHAR (12)   NULL,
    [FirstName]                          NVARCHAR (MAX) NULL,
    [LastName]                           NVARCHAR (MAX) NULL,
    [CrmProjectId]                       INT            NULL,
    [TaskId]                             INT            NULL,
    [Description]                        TEXT           NULL,
    [TeamLeaderId]                       INT            NULL,
    [LastSyncedUtc]                      DATETIME       NULL,
    [SyncVersion]                        INT            NOT NULL,
    [SuperOfficeId]                      INT            NULL,
    CONSTRAINT [PK_CrmReplicationAppointmentHistory] PRIMARY KEY CLUSTERED ([CrmReplicationAppointmentHistoryId] ASC) WITH (FILLFACTOR = 90)
);

