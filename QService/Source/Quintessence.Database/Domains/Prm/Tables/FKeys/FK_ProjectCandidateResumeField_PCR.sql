ALTER TABLE [dbo].[ProjectCandidateResumeField]
	ADD CONSTRAINT [FK_ProjectCandidateResumeField_ProjectCandidateResume]
	FOREIGN KEY (ProjectCandidateResumeId)
	REFERENCES [ProjectCandidateResume] (Id)
