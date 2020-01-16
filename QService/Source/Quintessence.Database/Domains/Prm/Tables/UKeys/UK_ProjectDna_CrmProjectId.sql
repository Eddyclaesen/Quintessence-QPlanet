ALTER TABLE [dbo].[ProjectDna]
	ADD CONSTRAINT [UK_ProjectDna_CrmProjectId] 
	UNIQUE ([CrmProjectId])