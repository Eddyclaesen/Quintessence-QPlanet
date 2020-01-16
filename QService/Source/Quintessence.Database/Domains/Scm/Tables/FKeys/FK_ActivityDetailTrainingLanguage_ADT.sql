ALTER TABLE [dbo].[ActivityDetailTrainingLanguage]  
	ADD CONSTRAINT [FK_ActivityDetailTrainingLanguage_ActivityDetailTraining] 
	FOREIGN KEY([ActivityDetailTrainingId])
	REFERENCES [dbo].[ActivityDetailTraining] ([Id])