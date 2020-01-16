ALTER TABLE [dbo].[SubProject]
	ADD CONSTRAINT [PK_SubProject] 
	PRIMARY KEY NONCLUSTERED ([ProjectId], [SubProjectId])