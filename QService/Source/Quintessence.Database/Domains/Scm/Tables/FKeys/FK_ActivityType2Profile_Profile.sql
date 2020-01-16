ALTER TABLE [dbo].[ActivityType2Profile]  
	ADD CONSTRAINT [FK_ActivityType2Profile_Profile] 
	FOREIGN KEY([ProfileId])
	REFERENCES [dbo].[Profile] ([Id])