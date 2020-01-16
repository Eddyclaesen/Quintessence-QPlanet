ALTER TABLE [dbo].[ActivityDetailConsulting]  
	ADD CONSTRAINT [FK_ActivityDetail_ActivityDetailConsulting] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ActivityDetail] ([Id])