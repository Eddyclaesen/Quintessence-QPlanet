ALTER TABLE [dbo].[ProjectCandidateResumeField]
	ADD CONSTRAINT [FK_ProjectCandidateResumeField_CandidateReportDefinitionField]
	FOREIGN KEY (CandidateReportDefinitionFieldId)
	REFERENCES [CandidateReportDefinitionField] (Id)
