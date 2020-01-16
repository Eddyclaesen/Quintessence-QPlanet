ALTER TABLE [dbo].[ProgramComponent]  
	ADD CONSTRAINT [FK_ProgramComponent_ProjectCandidate] 
	FOREIGN KEY([ProjectCandidateId])
	REFERENCES [dbo].[ProjectCandidate] ([Id])