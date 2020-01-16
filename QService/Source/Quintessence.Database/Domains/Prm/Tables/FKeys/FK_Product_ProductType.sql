ALTER TABLE [dbo].[Product]  
	ADD CONSTRAINT [FK_Product_ProductType] 
	FOREIGN KEY([ProductTypeId])
	REFERENCES [dbo].[ProductType] ([Id])