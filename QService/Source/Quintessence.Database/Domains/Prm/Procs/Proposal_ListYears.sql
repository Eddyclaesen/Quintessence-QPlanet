CREATE PROCEDURE [dbo].[Proposal_ListYears]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		DISTINCT Year([ProposalView].[DateReceived])

	FROM		[ProposalView]

	WHERE		[ProposalView].[DateReceived] IS NOT NULL

	UNION
	
	SELECT		DISTINCT Year([ProposalView].[Deadline])

	FROM		[ProposalView]

	WHERE		[ProposalView].[Deadline] IS NOT NULL

	UNION
	
	SELECT		DISTINCT Year([ProposalView].[DateSent])

	FROM		[ProposalView]

	WHERE		[ProposalView].[DateSent] IS NOT NULL
END
GO
