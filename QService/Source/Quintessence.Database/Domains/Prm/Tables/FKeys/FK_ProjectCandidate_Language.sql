ALTER TABLE [dbo].[ProjectCandidate]
	ADD CONSTRAINT [FK_ProjectCandidate_Language]
	FOREIGN KEY (ReportLanguageId)
	REFERENCES [Language] (Id)
