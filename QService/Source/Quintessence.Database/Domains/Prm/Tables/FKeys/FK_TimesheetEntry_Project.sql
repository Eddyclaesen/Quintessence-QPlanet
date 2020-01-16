ALTER TABLE [dbo].[TimesheetEntry]  
	ADD CONSTRAINT [FK_TimesheetEntry_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])