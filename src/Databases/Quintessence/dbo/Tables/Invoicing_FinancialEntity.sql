CREATE TABLE [dbo].[Invoicing_FinancialEntity] (
    [Id]                        UNIQUEIDENTIFIER NOT NULL,
    [Name]                      NVARCHAR (100)   NOT NULL,
    [CrmContactId]              INT              NOT NULL,
    [BusinessCentralCustomerId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Invoicing_FinancialEntity] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Invoicing_FinancialEntity_BusinessCentral_Customer] FOREIGN KEY ([BusinessCentralCustomerId]) REFERENCES [dbo].[BusinessCentral_Customer] ([Id]),
    CONSTRAINT [FK_Invoicing_FinancialEntity_CrmReplicationContact] FOREIGN KEY ([CrmContactId]) REFERENCES [dbo].[CrmReplicationContact] ([Id])
);


GO
CREATE CLUSTERED INDEX [IX_Invoicing_FinancialEntity_CrmContactId]
    ON [dbo].[Invoicing_FinancialEntity]([CrmContactId] ASC);

