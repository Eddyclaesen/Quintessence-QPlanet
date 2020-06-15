CREATE FUNCTION [dbo].[ProjectCandidate_GetLastTheoremListIsCompleted](
	@ProjectCandidateId UNIQUEIDENTIFIER
)
RETURNS BIT
AS
BEGIN
	RETURN
	(
		SELECT		TOP 1 [TheoremListView].[IsCompleted] 

		FROM		[ProjectCandidateView]

		INNER JOIN	[TheoremListRequestView] 
			ON		[TheoremListRequestView].[ProjectId] = [ProjectCandidateView].[ProjectId]
			AND		[TheoremListRequestView].[CandidateId] = [ProjectCandidateView].[CandidateId]
			AND		[TheoremListRequestView].[TheoremListRequestTypeId] = 1 --only as is

		INNER JOIN	[TheoremListView]
			ON		[TheoremListView].[TheoremListRequestId] = [TheoremListRequestView].[Id]

		WHERE		[ProjectCandidateView].[Id] = @ProjectCandidateId

		ORDER BY	[TheoremListRequestView].[RequestDate] DESC
	)
END