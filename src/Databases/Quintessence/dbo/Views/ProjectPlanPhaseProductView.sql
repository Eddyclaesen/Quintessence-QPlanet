
CREATE VIEW [dbo].[ProjectPlanPhaseProductView] AS
	SELECT		[ProjectPlanPhaseProduct].[Id],
				[ProjectPlanPhaseProduct].[ProductId],
				[ProjectPlanPhaseProduct].[Notes],
				[ProductView].[Name]							AS	ProductName,
				[ProductView].[ProductTypeId]					AS	ProductTypeId,
				[ProductView].[ProductTypeName]					AS	ProductTypeName,
				[ProjectPlanPhaseProduct].[UnitPrice]				AS	UnitPrice,
				[ProjectPlanPhaseProduct].[TotalPrice]				AS	TotalPrice,
				[ProjectPlanPhaseProduct].[NoInvoice]				AS	NoInvoice,
				[ProjectPlanPhaseProduct].[ProductsheetEntryId]			AS	ProductsheetEntryId

	FROM		[ProjectPlanPhaseProduct]	WITH (NOLOCK)
	INNER JOIN	[ProjectPlanPhaseEntryView]
		ON		[ProjectPlanPhaseEntryView].[Id] = [ProjectPlanPhaseProduct].[Id]

	INNER JOIN	[ProductView]
		ON		[ProductView].[Id] = [ProjectPlanPhaseProduct].[ProductId]

