CREATE VIEW [dbo].[ProjectCategoryPsDetailView] AS
	SELECT		[ProjectCategoryPsDetail].*

	FROM		[ProjectCategoryPsDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryPsDetail].[Id]