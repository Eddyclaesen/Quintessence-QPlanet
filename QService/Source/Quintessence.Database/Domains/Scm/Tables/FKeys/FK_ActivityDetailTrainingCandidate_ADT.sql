ALTER TABLE [dbo].[ActivityDetailTrainingCandidate]  
	ADD CONSTRAINT [FK_ActivityDetailTrainingCandidate_ActivityDetailTraining] 
	FOREIGN KEY([ActivityDetailTrainingId])
	REFERENCES [dbo].[ActivityDetailTraining] ([Id])