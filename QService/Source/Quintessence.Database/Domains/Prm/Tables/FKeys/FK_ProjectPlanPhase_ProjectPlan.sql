ALTER TABLE [dbo].[ProjectPlanPhase]  
	ADD CONSTRAINT [FK_ProjectPlanPhase_ProjectPlan] 
	FOREIGN KEY([ProjectPlanId])
	REFERENCES [dbo].[ProjectPlan] ([Id])