CREATE TABLE [dbo].[ProjectDocumentMetadata] (
    [ProjectId]        UNIQUEIDENTIFIER NOT NULL,
    [DocumentUniqueId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProjectDocumentMetadata] PRIMARY KEY NONCLUSTERED ([ProjectId] ASC, [DocumentUniqueId] ASC),
    CONSTRAINT [FK_ProjectDocumentMetadata_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

