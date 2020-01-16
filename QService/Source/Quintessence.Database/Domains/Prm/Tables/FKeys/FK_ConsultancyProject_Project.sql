ALTER TABLE [dbo].[ConsultancyProject]  
	ADD CONSTRAINT [FK_ConsultancyProject_Project] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[Project] ([Id])