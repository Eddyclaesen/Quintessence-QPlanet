ALTER TABLE [dbo].[ProjectDna2CrmPerson]  
	ADD CONSTRAINT [FK_ProjectDna2CrmPerson_ProjectDna] 
	FOREIGN KEY([ProjectDnaId])
	REFERENCES [dbo].[ProjectDna] ([Id])