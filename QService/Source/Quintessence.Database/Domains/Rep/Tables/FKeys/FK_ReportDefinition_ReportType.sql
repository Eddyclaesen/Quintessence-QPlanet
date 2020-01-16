ALTER TABLE [dbo].[ReportDefinition]  
	ADD CONSTRAINT [FK_ReportDefinition_ReportType] 
	FOREIGN KEY([ReportTypeId])
	REFERENCES [dbo].[ReportType] ([Id])