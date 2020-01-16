ALTER TABLE [dbo].[ProjectProduct]
	ADD CONSTRAINT [FK_ProjectProduct_Project]
	FOREIGN KEY (ProjectId)
	REFERENCES [Project] (Id)
