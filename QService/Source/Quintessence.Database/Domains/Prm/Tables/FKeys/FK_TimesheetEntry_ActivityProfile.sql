ALTER TABLE [dbo].[TimesheetEntry]  
	ADD CONSTRAINT [FK_TimesheetEntry_ActivityProfile] 
	FOREIGN KEY([ActivityProfileId])
	REFERENCES [dbo].[ActivityProfile] ([Id])