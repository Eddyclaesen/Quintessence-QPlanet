CREATE PROCEDURE ProjectCandidate_DeleteClusterScores
	@ProjectCandidateId		UNIQUEIDENTIFIER,
	@UserName				NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM	[ProjectCandidateClusterScore]
	WHERE		[ProjectCandidateClusterScore].[ProjectCandidateId] = @ProjectCandidateId
END