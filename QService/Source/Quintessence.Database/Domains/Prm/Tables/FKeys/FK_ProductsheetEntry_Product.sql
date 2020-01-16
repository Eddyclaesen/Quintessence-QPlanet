ALTER TABLE [dbo].[ProductsheetEntry]  
	ADD CONSTRAINT [FK_ProductsheetEntry_Product] 
	FOREIGN KEY([ProductId])
	REFERENCES [dbo].[Product] ([Id])