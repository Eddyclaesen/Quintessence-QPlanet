ALTER TABLE [dbo].[ActivityProfile]  
	ADD CONSTRAINT [FK_ActivityProfile_Profile] 
	FOREIGN KEY([ProfileId])
	REFERENCES [dbo].[Profile] ([Id])