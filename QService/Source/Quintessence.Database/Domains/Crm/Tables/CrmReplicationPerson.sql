CREATE TABLE [dbo].[CrmReplicationPerson](
	[Id]							INT			NOT NULL,
	[ContactId]						INT,
	[FirstName]						NVARCHAR(MAX),
	[LastName]						NVARCHAR(MAX),
	[Title]							NVARCHAR(MAX),
	[IsRetired]						BIT
)