ALTER TABLE [dbo].[Project2CrmProject]  
	ADD CONSTRAINT [FK_Project2CrmProject_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])