ALTER TABLE [dbo].[Project2TheoremListRequest]
	ADD CONSTRAINT [FK_Project2TheoremListRequest_TheoremListRequest]
	FOREIGN KEY (TheoremListRequestId)
	REFERENCES [TheoremListRequest] (Id)
