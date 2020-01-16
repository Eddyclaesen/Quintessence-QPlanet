ALTER TABLE [dbo].[TimesheetEntry]  
	ADD CONSTRAINT [FK_TimesheetEntry_User] 
	FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])