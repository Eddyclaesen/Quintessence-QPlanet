CREATE TABLE [dbo].[JobDefinition] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (200)   NOT NULL,
    [Assembly]  NVARCHAR (MAX)   NOT NULL,
    [Class]     NVARCHAR (MAX)   NOT NULL,
    [IsEnabled] BIT              NOT NULL
);

