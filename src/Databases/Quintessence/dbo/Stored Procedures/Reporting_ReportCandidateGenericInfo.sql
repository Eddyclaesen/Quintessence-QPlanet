CREATE PROCEDURE [dbo].[Reporting_ReportCandidateGenericInfo] 
	@ProjectCandidateId UNIQUEIDENTIFIER
AS
BEGIN
SET NOCOUNT ON

SELECT		[ProjectCandidateView].[CandidateId]							AS	[CandidateId]
			,[ProjectCandidateView].CandidateFirstName+' '+ProjectCandidateView.CandidateLastName AS [Candidate]
			,[ProjectCandidateView].CandidateGender							AS	[Gender]
			,[ProjectCandidateView].[ProjectId]								AS	[ProjectId]
			,CASE [ProjectCandidateView].[ReportLanguageId]
				WHEN 1 THEN [AssessmentDevelopmentProjectView].[FunctionTitle]
				WHEN 2 THEN ISNULL([AssessmentDevelopmentProjectView].FunctionTitleFR,[AssessmentDevelopmentProjectView].[FunctionTitle])
				WHEN 3 THEN ISNULL([AssessmentDevelopmentProjectView].FunctionTitleEN,[AssessmentDevelopmentProjectView].[FunctionTitle])
				ELSE [AssessmentDevelopmentProjectView].[FunctionTitle]
				END															AS	[FunctionTitle]
			,[ProjectCandidateDetailView].[AssessmentStartDate]				AS	[AssessmentDate]
			,[UA].[FirstName]+' '+[UA].[LastName]							AS	[Proma]
			,[UC].[FirstName]+' '+[UC].[LastName]							AS	[CustomerAssistant]
			,[UR].[FirstName]+' '+[UR].[LastName]							AS	[ReportReviewer]
			,[ProjectView].[ContactId]										AS	[ContactId]
			,CASE WHEN CrmContactView.[Name] LIKE 'BDO%' THEN 'BDO'
				ELSE CrmContactView.[Name] END 								AS	[CompanyName]
			,[DictionaryView].[ContactId]									AS	[DictionaryContactId]
			,[ProjectCandidateView].[ReportLanguageId]						AS	[ReportLanguageId]
			,[ProjectCandidateView].CandidateLanguageId						AS	[CandidateLanguageId]

FROM		[ProjectCandidateView]

INNER JOIN	[ProjectView] 
	ON		[ProjectView].[Id] = [ProjectCandidateView].[ProjectId]

INNER JOIN	[UserView] [UA] 
	ON		[UA].[Id] = [ProjectView].[ProjectManagerId]

INNER JOIN	[UserView] [UC] 
	ON		[UC].[Id] = [ProjectView].[CustomerAssistantId]

LEFT JOIN	[UserView] [UR] 
	ON		[UR].[Id] = [ProjectCandidateView].[ReportReviewerId]

INNER JOIN	[ProjectCandidateDetailView] 
	ON		[ProjectCandidateDetailView].[Id] = [ProjectCandidateView].[Id]

INNER JOIN	[AssessmentDevelopmentProjectView] 
	ON		[AssessmentDevelopmentProjectView].[Id] = [ProjectCandidateView].[ProjectId]

LEFT JOIN	[DictionaryView]
	ON		[DictionaryView].[Id] = [AssessmentDevelopmentProjectView].[DictionaryId]
	
INNER JOIN	CrmContactView
	ON		CrmContactView.[Id] = [ProjectView].[ContactId]

WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
END