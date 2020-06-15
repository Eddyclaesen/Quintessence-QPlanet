CREATE VIEW [dbo].[ProjectCategoryDetailType2View] AS
	SELECT		[ProjectCategoryDetailType2].*

	FROM		[ProjectCategoryDetailType2]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDetailType2].[Id]