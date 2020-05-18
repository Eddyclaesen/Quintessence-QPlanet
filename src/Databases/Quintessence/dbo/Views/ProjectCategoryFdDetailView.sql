CREATE VIEW [dbo].[ProjectCategoryFdDetailView] AS
	SELECT		[ProjectCategoryFdDetail].*

	FROM		[ProjectCategoryFdDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryFdDetail].[Id]