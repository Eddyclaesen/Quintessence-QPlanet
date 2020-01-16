CREATE TABLE [dbo].[AssessmentDevelopmentProject](
	[Id]								UNIQUEIDENTIFIER	NOT NULL,
	[FunctionTitle]						NVARCHAR(MAX)		NULL,
	[FunctionInformation]				TEXT				NULL,
	[DictionaryId]						UNIQUEIDENTIFIER	NULL,
	[CandidateReportDefinitionId]		UNIQUEIDENTIFIER	NULL, 
    [CandidateScoreReportTypeId]		INT					NOT NULL	DEFAULT 2, 
    [ReportDeadlineStep]				INT					NULL		DEFAULT 2,
	[PhoneCallRemarks]					NVARCHAR(MAX)		NULL,
	[ReportRemarks]						NVARCHAR(MAX)		NULL,
	[IsRevisionByPmRequired]			BIT					NOT NULL	DEFAULT 0,
	[SendReportToParticipant]			BIT					NOT NULL	DEFAULT 0,
	[SendReportToParticipantRemarks]	NVARCHAR(MAX)		NULL
)