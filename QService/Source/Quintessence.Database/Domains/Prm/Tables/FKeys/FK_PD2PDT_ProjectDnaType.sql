ALTER TABLE [dbo].[ProjectDna2ProjectDnaType]  
	ADD CONSTRAINT [FK_ProjectDna2ProjectDnaType_ProjectDnaType] 
	FOREIGN KEY([ProjectDnaTypeId])
	REFERENCES [dbo].[ProjectDnaType] ([Id])