ALTER TABLE [dbo].[TheoremListRequest]
	ADD CONSTRAINT [FK_TheoremListRequest_ContactPerson]
	FOREIGN KEY (ContactPersonId)
	REFERENCES [ContactPerson] (Id)
