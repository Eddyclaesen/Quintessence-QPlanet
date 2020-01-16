ALTER TABLE [dbo].[ProjectType2ProjectTypeCategory]  
	ADD CONSTRAINT [FK_ProjectType2ProjectCategory_ProjectCategory] 
	FOREIGN KEY([ProjectTypeCategoryId])
	REFERENCES [dbo].[ProjectTypeCategory] ([Id])