CREATE VIEW [dbo].[ActivityDetailTrainingTypeView] AS
	SELECT		[ActivityDetailTraining2TrainingType].[ActivityDetailTrainingId],
				[TrainingTypeView].*

	FROM		[ActivityDetailTraining2TrainingType]	WITH (NOLOCK)

	INNER JOIN	[TrainingTypeView]
		ON		[TrainingTypeView].[Id] = [ActivityDetailTraining2TrainingType].[TrainingTypeId]