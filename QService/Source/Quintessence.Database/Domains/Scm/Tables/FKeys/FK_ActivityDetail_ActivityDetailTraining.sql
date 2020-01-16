ALTER TABLE [dbo].[ActivityDetailTraining]  
	ADD CONSTRAINT [FK_ActivityDetail_ActivityDetailTraining] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ActivityDetail] ([Id])