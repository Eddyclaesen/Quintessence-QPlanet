CREATE TABLE [dbo].[CrmReplicationEmail](
	[Id]					INT			NOT NULL,
	[PersonId]				INT,
	[ContactId]				INT,
	[ContactName]			NVARCHAR(MAX),
	[FirstName]				NVARCHAR(MAX),
	[LastName]				NVARCHAR(MAX),
	[Email]					NVARCHAR(MAX),
	[DirectPhone]			NVARCHAR(MAX),
	[MobilePhone]			NVARCHAR(MAX)
)