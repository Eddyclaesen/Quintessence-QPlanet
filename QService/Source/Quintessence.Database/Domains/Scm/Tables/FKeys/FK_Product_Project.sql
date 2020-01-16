ALTER TABLE [dbo].[Product]  
	ADD CONSTRAINT [FK_Product_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])