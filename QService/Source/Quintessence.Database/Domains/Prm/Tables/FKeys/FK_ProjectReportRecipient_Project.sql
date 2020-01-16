ALTER TABLE [dbo].[ProjectReportRecipient]
	ADD CONSTRAINT [FK_ProjectReportRecipient_Project]
	FOREIGN KEY (ProjectId)
	REFERENCES [Project] (Id)
