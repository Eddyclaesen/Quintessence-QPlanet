ALTER TABLE [dbo].[ActivityDetailSupport]  
	ADD CONSTRAINT [FK_ActivityDetail_ActivityDetailSupport] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ActivityDetail] ([Id])