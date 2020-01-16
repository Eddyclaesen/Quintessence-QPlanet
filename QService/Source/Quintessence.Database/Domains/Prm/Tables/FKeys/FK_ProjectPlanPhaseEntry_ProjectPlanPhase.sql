ALTER TABLE [dbo].[ProjectPlanPhaseEntry]  
	ADD CONSTRAINT [FK_ProjectPlanPhaseEntry_ProjectPlanPhase] 
	FOREIGN KEY([ProjectPlanPhaseId])
	REFERENCES [dbo].[ProjectPlanPhase] ([Id])