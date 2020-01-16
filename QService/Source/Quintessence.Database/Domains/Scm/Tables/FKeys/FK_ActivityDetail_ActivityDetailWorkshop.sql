ALTER TABLE [dbo].[ActivityDetailWorkshop]  
	ADD CONSTRAINT [FK_ActivityDetail_ActivityDetailWorkshop] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ActivityDetail] ([Id])