ALTER TABLE [dbo].[ProjectCategoryDetail2Competence2Combination]  
	ADD CONSTRAINT [FK_ProjectCategoryDetail2C2C_DictionaryCompetence] 
	FOREIGN KEY([DictionaryCompetenceId])
	REFERENCES [dbo].[DictionaryCompetence] ([Id])