ALTER TABLE [dbo].[ProjectRoleTranslation]
	ADD CONSTRAINT [FK_ProjectRoleTranslation_ProjectRole]
	FOREIGN KEY (ProjectRoleId)
	REFERENCES [ProjectRole] (Id)
