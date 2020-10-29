CREATE TABLE [dbo].[BusinessCentral_Customer] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (100)   NOT NULL,
    [IsDeleted] BIT              NOT NULL,
    CONSTRAINT [PK_BusinessCentral_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

