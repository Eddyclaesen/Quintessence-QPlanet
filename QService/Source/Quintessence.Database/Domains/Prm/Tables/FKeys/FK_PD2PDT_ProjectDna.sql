ALTER TABLE [dbo].[ProjectDna2ProjectDnaType]  
	ADD CONSTRAINT [FK_ProjectDna2ProjectDnaType_ProjectDna] 
	FOREIGN KEY([ProjectDnaId])
	REFERENCES [dbo].[ProjectDna] ([Id])