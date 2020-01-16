CREATE VIEW [dbo].[ProjectCandidateMailingView]
AS

SELECT		 [dbo].[ProjectCandidateView].[Id]			AS [Id]
			,[dbo].[CandidateView].[FirstName]			AS [FirstName]
			,[dbo].[CandidateView].[LastName]			AS [LastName]
			,[dbo].[CandidateView].[Gender]				AS [Gender]
			,[dbo].[CandidateView].[Email]				AS [Email]
			,[dbo].[CrmContactView].[name]				AS [Company]

FROM		 [dbo].[ProjectCandidateView]

INNER JOIN	 [dbo].[CandidateView]
	ON		 [dbo].[CandidateView].[Id] = [dbo].[ProjectCandidateView].[CandidateId]

INNER JOIN	 [dbo].[ProjectView]
	ON		 [dbo].[ProjectCandidateView].[ProjectId] = [dbo].[ProjectView].[Id]

INNER JOIN	 [dbo].[CrmContactView]
	ON		 [dbo].[ProjectView].[ContactId] = [dbo].[CrmContactView].[Id]
