CREATE TABLE [dbo].[Operation] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Domain]      VARCHAR (3)      NOT NULL,
    [Code]        VARCHAR (10)     NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Operation] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

