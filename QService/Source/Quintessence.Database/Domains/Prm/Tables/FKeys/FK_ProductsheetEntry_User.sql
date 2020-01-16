ALTER TABLE [dbo].[ProductsheetEntry]  
	ADD CONSTRAINT [FK_ProductsheetEntry_User] 
	FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])