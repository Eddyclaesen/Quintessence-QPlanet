ALTER TABLE [dbo].[ProjectProduct]
	ADD CONSTRAINT [FK_ProjectProduct_ProductType]
	FOREIGN KEY (ProductTypeId)
	REFERENCES [ProductType] (Id)
