ALTER TABLE [dbo].[ReportParameterValue]  
	ADD CONSTRAINT [FK_ReportParameterValue_ReportParameter] 
	FOREIGN KEY([ReportParameterId])
	REFERENCES [dbo].[ReportParameter] ([Id])