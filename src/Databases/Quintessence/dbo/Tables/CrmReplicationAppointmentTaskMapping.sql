CREATE TABLE [dbo].[CrmReplicationAppointmentTaskMapping] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [TaskId]      INT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CrmReplicationAppointmentTaskMapping] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
);

