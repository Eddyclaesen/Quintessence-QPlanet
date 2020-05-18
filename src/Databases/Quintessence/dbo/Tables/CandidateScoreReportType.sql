CREATE TABLE [dbo].[CandidateScoreReportType] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    [Code] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_CandidateScoreReportType] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

