CREATE TABLE [dbo].[CrmReplicationProject](
	[Id]						INT					NOT NULL,
	[Name]						NVARCHAR(MAX),
	[AssociateId]				INT,
	[ContactId]					INT,
	[ProjectStatusId]			INT,
	[StartDate]					DATETIME,
	[BookyearFrom]				DATETIME			NULL,
	[BookyearTo]				DATETIME			NULL
)