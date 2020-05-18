CREATE TABLE [dbo].[ReportStatus] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Code]      NVARCHAR (25)  NOT NULL,
    [SortOrder] INT            NOT NULL,
    CONSTRAINT [PK_ReportStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

