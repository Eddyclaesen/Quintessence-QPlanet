CREATE VIEW [dbo].[ProjectCategoryFaDetailView] AS
	SELECT		[ProjectCategoryFaDetail].*

	FROM		[ProjectCategoryFaDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryFaDetail].[Id]
