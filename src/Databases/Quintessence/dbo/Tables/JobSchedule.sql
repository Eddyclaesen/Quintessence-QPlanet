CREATE TABLE [dbo].[JobSchedule] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [JobDefinitionId] UNIQUEIDENTIFIER NOT NULL,
    [StartTime]       TIME (7)         NOT NULL,
    [EndTime]         TIME (7)         NOT NULL,
    [Interval]        INT              NOT NULL,
    [IsEnabled]       BIT              NOT NULL
);

