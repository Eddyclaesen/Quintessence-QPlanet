ALTER TABLE [dbo].[ProjectDocumentMetadata]  
	ADD CONSTRAINT [FK_ProjectDocumentMetadata_Project] 
	FOREIGN KEY([ProjectId])
	REFERENCES [dbo].[Project] ([Id])