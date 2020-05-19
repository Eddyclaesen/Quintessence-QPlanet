CREATE TABLE [dbo].[SimulationSet2CrmContact] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CrmContactId]    INT              NOT NULL,
    [SimulationSetId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SimulationSet2CrmContact] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

