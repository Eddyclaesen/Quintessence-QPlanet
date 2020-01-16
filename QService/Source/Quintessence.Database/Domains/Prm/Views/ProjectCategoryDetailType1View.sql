CREATE VIEW [dbo].[ProjectCategoryDetailType1View] AS
	SELECT		[ProjectCategoryDetailType1].*

	FROM		[ProjectCategoryDetailType1]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType1].[Id]
