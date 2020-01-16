ALTER TABLE [dbo].[CandidateReportDefinitionField]  
	ADD CONSTRAINT [FK_CandidateReportDefinitionField_CandidateReportDefinition] 
	FOREIGN KEY([CandidateReportDefinitionId])
	REFERENCES [dbo].[CandidateReportDefinition] ([Id])