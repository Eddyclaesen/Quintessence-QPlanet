ALTER TABLE [dbo].[TheoremList]
	ADD CONSTRAINT [FK_TheoremList_TheoremListType]
	FOREIGN KEY (TheoremListTypeId)
	REFERENCES [TheoremListType] (Id)

