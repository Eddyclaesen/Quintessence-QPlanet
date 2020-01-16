ALTER TABLE [dbo].[ActivityDetailTraining2TrainingType]  
	ADD CONSTRAINT [FK_ActivityDetailTraining2TrainingType_ActivityDetailTraining] 
	FOREIGN KEY([ActivityDetailTrainingId])
	REFERENCES [dbo].[ActivityDetailTraining] ([Id])