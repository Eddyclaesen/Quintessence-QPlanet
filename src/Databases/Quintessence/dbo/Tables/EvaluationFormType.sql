﻿CREATE TABLE [dbo].[EvaluationFormType] (
    [Id]   INT            NOT NULL,
    [Code] NVARCHAR (10)  NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_EvaluationFormType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

