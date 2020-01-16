ALTER TABLE [dbo].[ProjectTypeCategoryUnitPrice]
	ADD CONSTRAINT [FK_ProjectTypeCategoryUnitPrice_ProjectTypeCategory]
	FOREIGN KEY (ProjectTypeCategoryId)
	REFERENCES [ProjectTypeCategory] (Id)
