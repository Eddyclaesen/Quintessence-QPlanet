CREATE PROCEDURE Reporting_ProjectCandidateRetrieveScoreReportTypeCode
	@ProjectCandidateId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		[CandidateScoreReportTypeView].[Code]

	FROM		[ProjectCandidateView]

	INNER JOIN	[AssessmentDevelopmentProjectView]
		ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

	INNER JOIN	[CandidateScoreReportTypeView]
		ON		[CandidateScoreReportTypeView].[Id] = [AssessmentDevelopmentProjectView].[CandidateScoreReportTypeId]

	WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
END
GO
