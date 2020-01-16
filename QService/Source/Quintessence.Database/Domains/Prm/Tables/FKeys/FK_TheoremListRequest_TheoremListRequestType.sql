ALTER TABLE [dbo].[TheoremListRequest]  
	ADD CONSTRAINT [FK_TheoremListRequest_TheoremListRequestType] 
	FOREIGN KEY([TheoremListRequestTypeId])
	REFERENCES [dbo].[TheoremListRequestType] ([Id])