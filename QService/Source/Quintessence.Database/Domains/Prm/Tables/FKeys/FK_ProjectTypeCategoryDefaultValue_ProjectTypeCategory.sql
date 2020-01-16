ALTER TABLE [dbo].[ProjectTypeCategoryDefaultValue]
	ADD CONSTRAINT [FK_ProjectTypeCategoryDefaultValue_ProjectTypeCategory]
	FOREIGN KEY (ProjectTypeCategoryId)
	REFERENCES [ProjectTypeCategory] (Id)
