ALTER TABLE [dbo].[ActivityProfile]  
	ADD CONSTRAINT [FK_ActivityProfile_Activity] 
	FOREIGN KEY([ActivityId])
	REFERENCES [dbo].[Activity] ([Id])