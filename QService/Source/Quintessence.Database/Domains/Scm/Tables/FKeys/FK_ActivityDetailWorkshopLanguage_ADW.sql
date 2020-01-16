ALTER TABLE [dbo].[ActivityDetailWorkshopLanguage]  
	ADD CONSTRAINT [FK_ActivityDetailWorkshopLanguage_ActivityDetailWorkshop] 
	FOREIGN KEY([ActivityDetailWorkshopId])
	REFERENCES [dbo].[ActivityDetailWorkshop] ([Id])