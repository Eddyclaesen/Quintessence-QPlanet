CREATE TABLE [dbo].[CrmReplicationContact](
	[Id]							INT			NOT NULL,
	[Name]							NVARCHAR(MAX),
	[Department]					NVARCHAR(MAX),
	[AssociateId]					INT,
	[AccountManagerId]				INT,
	[CustomerAssistantId]			INT
)