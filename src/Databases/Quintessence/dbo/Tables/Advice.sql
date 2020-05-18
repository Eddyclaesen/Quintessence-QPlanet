CREATE TABLE [dbo].[Advice] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (10)  NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Advice] PRIMARY KEY CLUSTERED ([Id] ASC)
);

