ALTER TABLE [dbo].[ActivityDetail]  
	ADD CONSTRAINT [FK_Activity_ActivityDetail] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[Activity] ([Id])