ALTER TABLE [dbo].[ProjectPlanPhaseProduct]  
	ADD CONSTRAINT [FK_ProjectPlanPhaseProduct_ProjectPlanPhaseEntry] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectPlanPhaseEntry] ([Id])