ALTER TABLE [dbo].[ActivityDetailCoaching]  
	ADD CONSTRAINT [FK_ActivityDetail_ActivityDetailCoaching] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[ActivityDetail] ([Id])