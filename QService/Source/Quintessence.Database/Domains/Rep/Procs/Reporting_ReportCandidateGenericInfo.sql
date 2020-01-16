CREATE PROCEDURE [dbo].[Reporting_ReportCandidateGenericInfo] 
	@ProjectCandidateId UNIQUEIDENTIFIER
AS
BEGIN

SELECT		[ProjectCandidateView].[CandidateId]							AS	[CandidateId]
			,[CandidateView].[FirstName]+' '+[CandidateView].[LastName]		AS	[Candidate]
			,[CandidateView].[Gender]										AS	[Gender]
			,[ProjectCandidateView].[ProjectId]								AS	[ProjectId]
			,[AssessmentDevelopmentProjectView].[FunctionTitle]				AS	[FunctionTitle]
			,[ProjectCandidateDetailView].[AssessmentStartDate]				AS	[AssessmentDate]
			,[UA].[FirstName]+' '+[UA].[LastName]							AS	[Proma]
			,[UC].[FirstName]+' '+[UC].[LastName]							AS	[CustomerAssistant]
			,[UR].[FirstName]+' '+[UR].[LastName]							AS	[ReportReviewer]
			,[ProjectView].[ContactId]										AS	[ContactId]
			,[CrmReplicationContact].[Name]									AS	[CompanyName]
			,[DictionaryView].[ContactId]									AS	[DictionaryContactId]
			,[ProjectCandidateView].[ReportLanguageId]						AS	[ReportLanguageId]
			,[CandidateView].[LanguageId]									AS	[CandidateLanguageId]

FROM		[ProjectCandidateView]

INNER JOIN	[CandidateView] 
	ON		[CandidateView].[Id] = [ProjectCandidateView].[CandidateId]

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
	
INNER JOIN	[CrmReplicationContact]
	ON		[CrmReplicationContact].[Id] = [ProjectView].[ContactId]

WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId
END
