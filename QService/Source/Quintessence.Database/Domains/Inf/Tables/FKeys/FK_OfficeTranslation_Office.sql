ALTER TABLE [dbo].[OfficeTranslation]  
	ADD CONSTRAINT [FK_OfficeTranslation_Office] 
	FOREIGN KEY([OfficeId])
	REFERENCES [dbo].[Office] ([Id])