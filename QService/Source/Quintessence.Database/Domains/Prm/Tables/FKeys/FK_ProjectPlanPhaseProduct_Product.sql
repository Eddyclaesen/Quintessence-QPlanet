ALTER TABLE [dbo].[ProjectPlanPhaseProduct]  
	ADD CONSTRAINT [FK_ProjectPlanPhaseProduct_Product] 
	FOREIGN KEY([ProductId])
	REFERENCES [dbo].[Product] ([Id])