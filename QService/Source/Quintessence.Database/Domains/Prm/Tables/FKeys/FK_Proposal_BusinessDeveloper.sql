ALTER TABLE [dbo].[Proposal]  
	ADD CONSTRAINT [FK_Project_BusinessDeveloper] 
	FOREIGN KEY([BusinessDeveloperId])
	REFERENCES [dbo].[User] ([Id])