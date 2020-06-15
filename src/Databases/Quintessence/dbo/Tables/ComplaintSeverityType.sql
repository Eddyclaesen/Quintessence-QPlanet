CREATE TABLE [dbo].[ComplaintSeverityType] (
    [Id]   INT            NOT NULL,
    [Code] NVARCHAR (10)  NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ComplaintSeverityType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

