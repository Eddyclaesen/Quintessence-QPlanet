ALTER TABLE [dbo].[Activity]  
	ADD CONSTRAINT [FK_Activity_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])