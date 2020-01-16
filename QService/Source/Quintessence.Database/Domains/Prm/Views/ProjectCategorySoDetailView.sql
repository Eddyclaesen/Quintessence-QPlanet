CREATE VIEW [dbo].[ProjectCategorySoDetailView] AS
	SELECT		[ProjectCategorySoDetail].*

	FROM		[ProjectCategorySoDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategorySoDetail].[Id]
