CREATE TABLE [dbo].[Office] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ShortName] NVARCHAR (2)   NOT NULL,
    [FullName]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Office] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

