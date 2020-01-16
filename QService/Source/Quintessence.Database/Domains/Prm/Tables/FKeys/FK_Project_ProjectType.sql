ALTER TABLE [dbo].[Project]  
	ADD CONSTRAINT [FK_Project_ProjectType] 
	FOREIGN KEY([ProjectTypeId])
	REFERENCES [dbo].[ProjectType] ([Id])