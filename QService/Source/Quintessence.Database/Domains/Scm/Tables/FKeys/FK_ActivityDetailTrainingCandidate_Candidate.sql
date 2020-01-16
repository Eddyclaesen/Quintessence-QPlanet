ALTER TABLE [dbo].[ActivityDetailTrainingCandidate]  
	ADD CONSTRAINT [FK_ActivityDetailTrainingCandidate_Candidate] 
	FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[Candidate] ([Id])