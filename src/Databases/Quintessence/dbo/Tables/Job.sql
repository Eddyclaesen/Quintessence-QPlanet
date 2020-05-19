CREATE TABLE [dbo].[Job] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [JobDefinitionId] UNIQUEIDENTIFIER NOT NULL,
    [JobScheduleId]   UNIQUEIDENTIFIER NULL,
    [RequestDate]     DATETIME         NOT NULL,
    [StartDate]       DATETIME         NULL,
    [EndDate]         DATETIME         NULL,
    [Success]         BIT              NULL
);

