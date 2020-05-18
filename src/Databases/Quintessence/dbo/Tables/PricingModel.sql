CREATE TABLE [dbo].[PricingModel] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_PricingModel] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

