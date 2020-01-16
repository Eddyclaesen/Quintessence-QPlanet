ALTER TABLE [dbo].[TheoremTemplate]
	ADD CONSTRAINT [FK_TheoremTemplate_TheoremListTemplate]
	FOREIGN KEY (TheoremListTemplateId)
	REFERENCES [TheoremListTemplate] (Id)
