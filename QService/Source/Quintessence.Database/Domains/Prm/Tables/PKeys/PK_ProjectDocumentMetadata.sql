ALTER TABLE [dbo].[ProjectDocumentMetadata]
	ADD CONSTRAINT [PK_ProjectDocumentMetadata] 
	PRIMARY KEY NONCLUSTERED ([ProjectId], [DocumentUniqueId])