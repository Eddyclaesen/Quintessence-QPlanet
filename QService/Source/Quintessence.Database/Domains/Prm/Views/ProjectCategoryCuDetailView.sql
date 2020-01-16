CREATE VIEW [dbo].[ProjectCategoryCuDetailView] AS
	SELECT		[ProjectCategoryCuDetail].*

	FROM		[ProjectCategoryCuDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryCuDetail].[Id]
