ALTER TABLE [dbo].[ProjectRevenueDistribution]
	ADD CONSTRAINT [UK_ProjectRevenueDistribution] 
	UNIQUE ([ProjectId], [CrmProjectId])