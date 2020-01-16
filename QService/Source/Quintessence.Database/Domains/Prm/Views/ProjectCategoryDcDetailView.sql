CREATE VIEW [dbo].[ProjectCategoryDcDetailView] AS
	SELECT		[ProjectCategoryDcDetail].*

	FROM		[ProjectCategoryDcDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryDcDetail].[Id]
