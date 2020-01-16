CREATE VIEW [dbo].[ProjectCategoryEaDetailView] AS
	SELECT		[ProjectCategoryEaDetail].*

	FROM		[ProjectCategoryEaDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryEaDetail].[Id]
