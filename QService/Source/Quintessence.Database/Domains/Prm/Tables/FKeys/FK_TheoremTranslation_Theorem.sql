ALTER TABLE [dbo].[TheoremTranslation]
	ADD CONSTRAINT [FK_TheoremTranslation_Theorem]
	FOREIGN KEY (TheoremId)
	REFERENCES [Theorem] (Id)
