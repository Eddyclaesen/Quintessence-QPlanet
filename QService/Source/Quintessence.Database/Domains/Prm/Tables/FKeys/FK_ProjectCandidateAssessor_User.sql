ALTER TABLE [dbo].[ProjectCandidateAssessor]
	ADD CONSTRAINT [FK_ProjectCandidateAssessor_User]
	FOREIGN KEY (UserId)
	REFERENCES [User] (Id)
