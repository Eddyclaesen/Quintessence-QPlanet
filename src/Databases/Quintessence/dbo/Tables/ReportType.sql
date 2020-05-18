CREATE TABLE [dbo].[ReportType] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    [Code] VARCHAR (10)   NOT NULL,
    CONSTRAINT [PK_ReportType] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

