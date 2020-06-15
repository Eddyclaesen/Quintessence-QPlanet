CREATE VIEW [dbo].[ActivityDetailConsultingView] AS
	SELECT		[ActivityDetailConsulting].*

	FROM		[ActivityDetailConsulting]	WITH (NOLOCK)

	INNER JOIN	[ActivityDetailView]
		ON		[ActivityDetailView].[Id] = [ActivityDetailConsulting].[Id]