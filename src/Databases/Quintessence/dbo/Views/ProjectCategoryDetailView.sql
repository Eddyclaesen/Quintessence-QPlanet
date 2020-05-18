CREATE VIEW [dbo].[ProjectCategoryDetailView] AS
	SELECT		[ProjectCategoryDetail].*,
				[ProjectView].[PricingModelId]			AS [PricingModelId]
	FROM		[ProjectCategoryDetail]	WITH (NOLOCK)

	INNER JOIN  [ProjectView]	
		ON		[ProjectView].[Id] = [ProjectCategoryDetail].[ProjectId]

	WHERE		[ProjectCategoryDetail].[Audit_IsDeleted] = 0