ALTER TABLE [dbo].[ProjectCandidateResume]
	ADD CONSTRAINT [FK_ProjectCandidateResume_Advice]
	FOREIGN KEY (AdviceId)
	REFERENCES [Advice] (Id)
