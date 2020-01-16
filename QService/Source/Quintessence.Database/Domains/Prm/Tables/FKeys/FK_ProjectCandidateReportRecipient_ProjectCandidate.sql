ALTER TABLE [dbo].[ProjectCandidateReportRecipient]
	ADD CONSTRAINT [FK_ProjectCandidateReportRecipient_ProjectCandidate]
	FOREIGN KEY (ProjectCandidateId)
	REFERENCES [ProjectCandidate] (Id)
