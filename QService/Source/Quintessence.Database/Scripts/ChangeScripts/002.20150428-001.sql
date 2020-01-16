ALTER TABLE dbo.ProjectProduct
ADD Deadline DATETIME NULL,
NoInvoice BIT NOT NULL DEFAULT 0

GO

ALTER VIEW [dbo].[ProjectProductView] AS
	SELECT		[ProjectProduct].*,
				[ProductTypeView].[Name]			AS	[ProductTypeName]
				
	FROM		[ProjectProduct]	WITH (NOLOCK)

	INNER JOIN	[ProductTypeView]
		ON		[ProductTypeView].[Id] = [ProjectProduct].[ProductTypeId]
	
	WHERE		[ProjectProduct].[Audit_IsDeleted] = 0

GO

ALTER TABLE dbo.ProjectPlanPhaseProduct
ADD UnitPrice DECIMAL(18,2) NULL,
TotalPrice DECIMAL(18,2) NULL,
NoInvoice BIT NOT NULL DEFAULT 0,
ProductsheetEntryId UNIQUEIDENTIFIER NULL

GO

ALTER VIEW [dbo].[ProjectPlanPhaseProductView] AS
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

GO

UPDATE [ProjectPlanPhaseProduct]
SET [ProjectPlanPhaseProduct].[UnitPrice] = 
(SELECT [ProductView].[UnitPrice] FROM [ProductView] WHERE [ProductView].[Id] = [ProjectPlanPhaseProduct].[ProductId])

UPDATE [ProjectPlanPhaseProduct]
SET [ProjectPlanPhaseProduct].[TotalPrice] = 
(SELECT [ProjectPlanPhaseProduct].[UnitPrice] * [ProjectPlanPhaseEntry].[Quantity] FROM [ProjectPlanPhaseEntry] WHERE [ProjectPlanPhaseEntry].[Id] = [ProjectPlanPhaseProduct].[Id])




--INSERT INTO ProductsheetEntry
--(Id, ProductId, ProjectPlanPhaseId, Quantity, InvoiceAmount, UserId, Name, ProjectId, InvoiceStatusCode, InvoiceRemarks, Date, Audit_CreatedBy, Audit_CreatedOn, Audit_VersionId)
--SELECT 
--NEWID() AS Id,
--Summary.ProductId,
--Summary.ProjectPlanPhaseId,
--Summary.TotalQuantity AS Quantity,
--Summary.TotalPrice AS InvoiceAmount,
--'C720B3B2-1E1C-4935-AFB3-1512B8337017' AS UserId, -- UsedId User: Sync
--Product.Name + ' (' + ProductType.Name + ')' AS Name,
--ConsultancyProject.Id AS ProjectId,
--10 AS InvoiceStatusCode,
--'Line from conversion' AS InvoiceRemarks,
--ProjectPlanPhase.EndDate AS Date,
--'Conversion' AS Audit_CreatedBy,
--GETDATE() AS Audit_CreatedOn,
--NEWID() AS Audit_VersionId
--FROM
--(SELECT ProductId, ProjectPlanPhaseId, SUM(Quantity) as TotalQuantity, SUM(Price) as TotalPrice
--FROM
--(SELECT
--ProjectPlanPhaseProduct.ProductId AS ProductId, 
--ProjectPlanPhaseEntry.ProjectPlanPhaseId AS ProjectPlanPhaseId, 
--ProjectPlanPhaseEntry.Quantity AS Quantity, 
--ProjectPlanPhaseProduct.TotalPrice AS Price
--FROM ProjectPlanPhaseProduct
--JOIN ProjectPlanPhaseEntry ON ProjectPlanPhaseProduct.Id = ProjectPlanPhaseEntry.Id
--WHERE NoInvoice = 0
--AND Audit_IsDeleted = 0
--UNION ALL
--SELECT 
--ProductsheetEntry.ProductId AS ProductId, 
--ProductsheetEntry.ProjectPlanPhaseId AS ProjectPlanPhaseId, 
---Quantity AS Quantity, 
---InvoiceAmount AS Price  
--FROM ProductsheetEntry
--WHERE Audit_IsDeleted = 0) as a
--group by ProductId, ProjectPlanPhaseId) as Summary
--JOIN Product ON Product.Id = Summary.ProductId
--JOIN ProductType ON ProductType.Id = Product.ProductTypeId
--JOIN ProjectPlanPhase ON ProjectPlanPhase.Id = Summary.ProjectPlanPhaseId
--JOIN ConsultancyProject ON ConsultancyProject.ProjectPlanId = ProjectPlanPhase.ProjectPlanId
--JOIN Project ON Project.Id = ConsultancyProject.Id
--WHERE TotalPrice > 0
--AND Project.PricingModelId = 1