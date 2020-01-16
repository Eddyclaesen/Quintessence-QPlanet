CREATE VIEW [dbo].[ProjectPlanPhaseProductView] AS
	SELECT		[ProjectPlanPhaseProduct].[Id],
				[ProjectPlanPhaseProduct].[ProductId],
				[ProjectPlanPhaseProduct].[Notes],
				[ProductView].[Name]							AS	ProductName,
				[ProductView].[ProductTypeId]					AS	ProductTypeId,
				[ProductView].[ProductTypeName]					AS	ProductTypeName,
				[ProductView].[UnitPrice]						AS	UnitPrice

	FROM		[ProjectPlanPhaseProduct]	WITH (NOLOCK)
	INNER JOIN	[ProjectPlanPhaseEntryView]
		ON		[ProjectPlanPhaseEntryView].[Id] = [ProjectPlanPhaseProduct].[Id]

	INNER JOIN	[ProductView]
		ON		[ProductView].[Id] = [ProjectPlanPhaseProduct].[ProductId]