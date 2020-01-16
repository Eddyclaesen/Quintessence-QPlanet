CREATE VIEW [dbo].[ActivityDetailCoachingView] AS
	SELECT		[ActivityDetailCoaching].*

	FROM		[ActivityDetailCoaching]	WITH (NOLOCK)

	INNER JOIN	[ActivityDetailView]
		ON		[ActivityDetailView].[Id] = [ActivityDetailCoaching].[Id]