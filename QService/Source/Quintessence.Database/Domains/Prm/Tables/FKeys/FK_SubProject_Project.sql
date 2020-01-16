ALTER TABLE [dbo].[SubProject]  
	ADD CONSTRAINT [FK_SubProject_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])