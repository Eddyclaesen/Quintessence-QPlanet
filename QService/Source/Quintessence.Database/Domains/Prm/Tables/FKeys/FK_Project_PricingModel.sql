ALTER TABLE [dbo].[Project]  
	ADD CONSTRAINT [FK_Project_PricingModel] 
	FOREIGN KEY([PricingModelId])
	REFERENCES [dbo].[PricingModel] ([Id])