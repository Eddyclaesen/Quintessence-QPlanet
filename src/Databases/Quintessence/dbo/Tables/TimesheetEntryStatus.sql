CREATE TABLE [dbo].[TimesheetEntryStatus] (
    [Id]          INT           NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Description] TEXT          NULL,
    CONSTRAINT [PK_TimesheetEntryStatus] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

