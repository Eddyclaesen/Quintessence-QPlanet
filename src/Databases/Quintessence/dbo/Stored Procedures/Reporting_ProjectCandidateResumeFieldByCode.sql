CREATE PROCEDURE Reporting_ProjectCandidateResumeFieldByCode
	@ProjectCandidateId	UNIQUEIDENTIFIER,
	@Code				NVARCHAR(MAX)
AS
BEGIN
	SELECT		[ProjectCandidateResumeFieldView].[Statement]

	FROM		[ProjectCandidateResumeFieldView]

	INNER JOIN	[ProjectCandidateResumeView]
		ON		[ProjectCandidateResumeView].[Id] = [ProjectCandidateResumeFieldView].[ProjectCandidateResumeId]
		AND		[ProjectCandidateResumeView].[ProjectCandidateId] = @ProjectCandidateId
	
	INNER JOIN	[CandidateReportDefinitionFieldView]
		ON		[CandidateReportDefinitionFieldView].[Id] = [ProjectCandidateResumeFieldView].[CandidateReportDefinitionFieldId]

	WHERE		[CandidateReportDefinitionFieldView].[Code] = @Code
END