ALTER TABLE [dbo].[ProductsheetEntry]  
	ADD CONSTRAINT [FK_ProductsheetEntry_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])