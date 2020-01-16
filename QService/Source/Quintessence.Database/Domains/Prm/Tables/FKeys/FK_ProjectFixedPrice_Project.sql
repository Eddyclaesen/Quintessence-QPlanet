ALTER TABLE [dbo].[ProjectFixedPrice]
	ADD CONSTRAINT [FK_ProjectFixedPrice_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])