CREATE PROCEDURE ProjectCandidate_DeleteIndicatorScores
	@ProjectCandidateId		UNIQUEIDENTIFIER,
	@UserName				NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [ProjectCandidateIndicatorScore]
	WHERE		[ProjectCandidateIndicatorScore].[ProjectCandidateId] = @ProjectCandidateId
END
GO