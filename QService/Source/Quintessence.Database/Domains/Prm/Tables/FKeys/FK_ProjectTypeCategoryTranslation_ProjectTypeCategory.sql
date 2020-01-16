ALTER TABLE [dbo].[ProjectTypeCategoryTranslation]  
	ADD CONSTRAINT [FK_ProjectTypeCategoryTranslation_ProjectTypeCategory] 
	FOREIGN KEY([ProjectTypeCategoryId])
	REFERENCES [dbo].[ProjectTypeCategory] ([Id])