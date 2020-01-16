ALTER TABLE [dbo].[ProjectRevenueDistribution]  
	ADD CONSTRAINT [FK_ProjectRevenueDistribution_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])