CREATE VIEW [dbo].[ActivityDetailSupportView] AS
	SELECT		[ActivityDetailSupport].*

	FROM		[ActivityDetailSupport]	WITH (NOLOCK)

	INNER JOIN	[ActivityDetailView]
		ON		[ActivityDetailView].[Id] = [ActivityDetailSupport].[Id]