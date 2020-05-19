CREATE PROCEDURE ProjectCandidate_DeleteCompetenceScores
	@ProjectCandidateId		UNIQUEIDENTIFIER,
	@UserName				NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [ProjectCandidateCompetenceScore]
	WHERE		[ProjectCandidateCompetenceScore].[ProjectCandidateId] = @ProjectCandidateId
END