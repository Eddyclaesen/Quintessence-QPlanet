CREATE TABLE [dbo].[CandidateHistory] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [ActId] INT              NOT NULL,
    CONSTRAINT [PK_CandidateHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

