CREATE TABLE [dbo].[ComplaintType] (
    [Id]   INT            NOT NULL,
    [Code] NVARCHAR (10)  NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ComplaintType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

