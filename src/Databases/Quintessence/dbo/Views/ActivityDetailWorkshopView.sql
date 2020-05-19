CREATE VIEW [dbo].[ActivityDetailWorkshopView] AS
	SELECT		[ActivityDetailWorkshop].*

	FROM		[ActivityDetailWorkshop]	WITH (NOLOCK)

	INNER JOIN	[ActivityDetailView]
		ON		[ActivityDetailView].[Id] = [ActivityDetailWorkshop].[Id]