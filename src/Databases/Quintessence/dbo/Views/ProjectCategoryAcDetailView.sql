CREATE VIEW [dbo].[ProjectCategoryAcDetailView] AS
	SELECT		[ProjectCategoryAcDetail].*

	FROM		[ProjectCategoryAcDetail]	WITH (NOLOCK)

	INNER JOIN	[ProjectCategoryDetailView]	
		ON		[ProjectCategoryDetailView].[Id] = [ProjectCategoryAcDetail].[Id]