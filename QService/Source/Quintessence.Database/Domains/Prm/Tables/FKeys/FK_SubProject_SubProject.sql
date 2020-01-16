ALTER TABLE [dbo].[SubProject]  
	ADD CONSTRAINT [FK_SubProject_SubProject] 
	FOREIGN KEY([SubProjectId])
	REFERENCES [dbo].[Project] ([Id])