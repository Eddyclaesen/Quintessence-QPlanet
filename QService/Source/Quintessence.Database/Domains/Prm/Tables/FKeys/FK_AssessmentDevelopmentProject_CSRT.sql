ALTER TABLE [dbo].[AssessmentDevelopmentProject]  
	ADD CONSTRAINT [FK_AssessmentDevelopmentProject_CandidateScoreReportType] 
	FOREIGN KEY([CandidateScoreReportTypeId])
	REFERENCES [dbo].[CandidateScoreReportType] ([Id])