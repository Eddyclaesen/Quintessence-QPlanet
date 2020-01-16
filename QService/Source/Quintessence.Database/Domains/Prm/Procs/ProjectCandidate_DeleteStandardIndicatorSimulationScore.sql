CREATE PROCEDURE ProjectCandidate_DeleteStandardIndicatorSimulationScore
	@Id						UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [ProjectCandidateIndicatorSimulationScore]
	WHERE		[ProjectCandidateIndicatorSimulationScore].[Id] = @Id
END
GO