ALTER TABLE [dbo].[AssessmentDevelopmentProject]  
	ADD CONSTRAINT [FK_AssessmentDevelopmentProject_CandidateReportDefinition] 
	FOREIGN KEY([CandidateReportDefinitionId])
	REFERENCES [dbo].[CandidateReportDefinition] ([Id])