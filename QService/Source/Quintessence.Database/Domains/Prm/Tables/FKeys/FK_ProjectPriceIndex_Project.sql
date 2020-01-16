ALTER TABLE [dbo].[ProjectPriceIndex]
	ADD CONSTRAINT [FK_ProjectPriceIndex_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])