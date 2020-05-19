CREATE TABLE [dbo].[CrmReplicationAppointmentTimesheetHistory] (
    [CrmReplicationAppointmentTimesheetHistoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Id]                                          INT            NOT NULL,
    [ProjectId]                                   INT            NULL,
    [AssociateId]                                 INT            NULL,
    [StartDate]                                   DATETIME2 (7)  NULL,
    [EndDate]                                     DATETIME2 (7)  NULL,
    [Description]                                 NVARCHAR (MAX) NULL,
    [TaskDescription]                             NVARCHAR (150) NULL,
    [TeamLeaderId]                                INT            NULL,
    [LastSyncedUtc]                               DATETIME       NULL,
    [SyncVersion]                                 INT            NOT NULL,
    [SuperOfficeId]                               INT            NULL,
    CONSTRAINT [PK_CrmReplicationAppointmentTimesheetHistory] PRIMARY KEY CLUSTERED ([CrmReplicationAppointmentTimesheetHistoryId] ASC) WITH (FILLFACTOR = 90)
);

