ALTER TABLE [dbo].[ProjectTypeCategoryUnitPrice]
	ADD CONSTRAINT [FK_ProjectTypeCategoryUnitPrice_ProjectTypeCategoryLevel]
	FOREIGN KEY (ProjectTypeCategoryLevelId)
	REFERENCES [ProjectTypeCategoryLevel] (Id)
