CREATE VIEW [dbo].[ActivityDetailView] AS
	SELECT		[ActivityDetail].*

	FROM		[ActivityDetail]	WITH (NOLOCK)

	INNER JOIN	[ActivityView]
		ON		[ActivityView].[Id] = [ActivityDetail].[Id]