CREATE PROCEDURE ProjectCandidate_DeleteFocusedIndicatorSimulationScore
	@Id						UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [ProjectCandidateIndicatorSimulationScore]
	WHERE		[ProjectCandidateIndicatorSimulationScore].[Id] = @Id
END
GO