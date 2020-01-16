ALTER TABLE [dbo].[Theorem]
	ADD CONSTRAINT [FK_Theorem_TheoremList]
	FOREIGN KEY (TheoremListId)
	REFERENCES [TheoremList] (Id)
