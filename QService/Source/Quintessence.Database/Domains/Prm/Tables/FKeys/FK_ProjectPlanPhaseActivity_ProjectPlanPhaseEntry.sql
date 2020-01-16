ALTER TABLE [dbo].[ProjectPlanPhaseActivity]  
	ADD CONSTRAINT [FK_ProjectPlanPhaseActivity_ProjectPlanPhaseEntry] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ProjectPlanPhaseEntry] ([Id])