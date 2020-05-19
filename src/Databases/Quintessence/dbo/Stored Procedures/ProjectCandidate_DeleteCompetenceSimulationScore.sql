CREATE PROCEDURE ProjectCandidate_DeleteCompetenceSimulationScore
	@Id						UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [ProjectCandidateCompetenceSimulationScore]
	WHERE		[ProjectCandidateCompetenceSimulationScore].[Id] = @Id
END