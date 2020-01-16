ALTER TABLE [dbo].[ActivityDetailTraining2TrainingType]  
	ADD CONSTRAINT [FK_ActivityDetailTraining2TrainingType_TrainingType] 
	FOREIGN KEY([TrainingTypeId])
	REFERENCES [dbo].[TrainingType] ([Id])