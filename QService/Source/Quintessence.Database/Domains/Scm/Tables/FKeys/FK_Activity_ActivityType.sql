ALTER TABLE [dbo].[Activity]  
	ADD CONSTRAINT [FK_Activity_ActivityType] 
	FOREIGN KEY([ActivityTypeId])
	REFERENCES [dbo].[ActivityType] ([Id])