ALTER TABLE [dbo].[ProjectCandidate]
	ADD CONSTRAINT [FK_ProjectCandidate_ReportStatus]
	FOREIGN KEY (ReportStatusId)
	REFERENCES [ReportStatus] (Id)
