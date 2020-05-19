﻿CREATE TABLE [dbo].[ProjectType] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Code] NVARCHAR (10)    NOT NULL,
    [Name] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_ProjectType] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

