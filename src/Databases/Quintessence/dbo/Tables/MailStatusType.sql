CREATE TABLE [dbo].[MailStatusType] (
    [Id]   INT            NOT NULL,
    [Code] NVARCHAR (10)  NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_MailStatusType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

