ALTER TABLE [dbo].[ProjectType2ProjectTypeCategory]  
	ADD CONSTRAINT [FK_ProjectType2ProjectTypeCategory_ProjectType] 
	FOREIGN KEY([ProjectTypeId])
	REFERENCES [dbo].[ProjectType] ([Id])