CREATE VIEW [dbo].[ProjectCategoryDetailType3View] AS
	SELECT		[ProjectCategoryDetailType3].*

	FROM		[ProjectCategoryDetailType3]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType3].[Id]