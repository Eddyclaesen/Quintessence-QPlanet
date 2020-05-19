CREATE VIEW [dbo].[ActivityDetailTrainingView] AS
	SELECT		[ActivityDetailTraining].[Id],
				[ActivityDetailTraining].[TargetGroup],
				[ActivityDetailTraining].[Duration],
				[ActivityDetailTraining].[ExtraInfo],
				[ActivityDetailTraining].[ChecklistLink],
				[ActivityDetailTraining].[Code]

	FROM		[ActivityDetailTraining]	WITH (NOLOCK)

	INNER JOIN	[ActivityDetailView]
		ON		[ActivityDetailView].[Id] = [ActivityDetailTraining].[Id]