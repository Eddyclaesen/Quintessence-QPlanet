ALTER TABLE [dbo].[EmailTemplate]
	ADD CONSTRAINT [FK_EmailTemplate_TheoremListRequestType]
	FOREIGN KEY (TheoremListRequestTypeId)
	REFERENCES [TheoremListRequestType] (Id)
