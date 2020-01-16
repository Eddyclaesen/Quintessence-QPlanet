ALTER TABLE [dbo].[ActivityType2Profile]  
	ADD CONSTRAINT [FK_ActivityType2Profile_ActivityType] 
	FOREIGN KEY([ActivityTypeId])
	REFERENCES [dbo].[ActivityType] ([Id])