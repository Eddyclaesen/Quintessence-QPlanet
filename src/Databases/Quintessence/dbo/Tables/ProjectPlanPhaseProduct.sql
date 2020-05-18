CREATE TABLE [dbo].[ProjectPlanPhaseProduct] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ProductId]           UNIQUEIDENTIFIER NOT NULL,
    [Notes]               NVARCHAR (MAX)   NULL,
    [UnitPrice]           DECIMAL (18, 2)  NULL,
    [TotalPrice]          DECIMAL (18, 2)  NULL,
    [NoInvoice]           BIT              DEFAULT ((0)) NOT NULL,
    [ProductsheetEntryId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProjectPlanPhaseProduct] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectPlanPhaseProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_ProjectPlanPhaseProduct_ProjectPlanPhaseEntry] FOREIGN KEY ([Id]) REFERENCES [dbo].[ProjectPlanPhaseEntry] ([Id])
);

