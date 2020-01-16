ALTER TABLE [dbo].[TheoremList]
	ADD CONSTRAINT [FK_TheoremList_TheoremListRequest]
	FOREIGN KEY (TheoremListRequestId)
	REFERENCES [TheoremListRequest] (Id)
