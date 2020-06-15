CREATE TABLE [dbo].[AssessmentDevelopmentProject] (
    [Id]                             UNIQUEIDENTIFIER NOT NULL,
    [FunctionTitle]                  NVARCHAR (MAX)   NULL,
    [FunctionInformation]            TEXT             NULL,
    [DictionaryId]                   UNIQUEIDENTIFIER NULL,
    [CandidateReportDefinitionId]    UNIQUEIDENTIFIER NULL,
    [CandidateScoreReportTypeId]     INT              DEFAULT ((2)) NOT NULL,
    [ReportDeadlineStep]             INT              DEFAULT ((2)) NULL,
    [PhoneCallRemarks]               NVARCHAR (MAX)   NULL,
    [ReportRemarks]                  NVARCHAR (MAX)   NULL,
    [IsRevisionByPmRequired]         BIT              DEFAULT ((0)) NOT NULL,
    [SendReportToParticipant]        BIT              DEFAULT ((0)) NOT NULL,
    [SendReportToParticipantRemarks] NVARCHAR (MAX)   NULL,
    [FunctionTitleEN]                NVARCHAR (MAX)   NULL,
    [FunctionTitleFR]                NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AssessmentDevelopmentProject] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssessmentDevelopmentProject_CandidateReportDefinition] FOREIGN KEY ([CandidateReportDefinitionId]) REFERENCES [dbo].[CandidateReportDefinition] ([Id]),
    CONSTRAINT [FK_AssessmentDevelopmentProject_CandidateScoreReportType] FOREIGN KEY ([CandidateScoreReportTypeId]) REFERENCES [dbo].[CandidateScoreReportType] ([Id]),
    CONSTRAINT [FK_AssessmentDevelopmentProject_Project] FOREIGN KEY ([Id]) REFERENCES [dbo].[Project] ([Id])
);

