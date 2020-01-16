ALTER TABLE [dbo].[TimesheetEntry]  
	ADD CONSTRAINT [FK_TimesheetEntry_ProjectPlanPhase] 
	FOREIGN KEY([ProjectPlanPhaseId])
	REFERENCES [dbo].[ProjectPlanPhase] ([Id])