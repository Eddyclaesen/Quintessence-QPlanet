CREATE VIEW [dbo].[TrainingTypeView] AS
	SELECT		[TrainingType].*

	FROM		[TrainingType]	WITH (NOLOCK)

	WHERE		[TrainingType].[Audit_IsDeleted] = 0