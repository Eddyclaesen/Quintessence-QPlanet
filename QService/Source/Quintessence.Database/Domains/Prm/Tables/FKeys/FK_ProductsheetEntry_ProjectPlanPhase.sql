ALTER TABLE [dbo].[ProductsheetEntry]  
	ADD CONSTRAINT [FK_ProductsheetEntry_ProjectPlanPhase] 
	FOREIGN KEY([ProjectPlanPhaseId])
	REFERENCES [dbo].[ProjectPlanPhase] ([Id])