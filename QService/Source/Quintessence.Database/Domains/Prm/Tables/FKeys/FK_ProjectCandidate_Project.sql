ALTER TABLE [dbo].[ProjectCandidate]
	ADD CONSTRAINT [FK_ProjectCandidate_Project]
	FOREIGN KEY (ProjectId)
	REFERENCES [Project] (Id)
