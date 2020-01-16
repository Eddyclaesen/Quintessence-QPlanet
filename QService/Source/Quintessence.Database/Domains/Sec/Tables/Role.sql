CREATE TABLE [dbo].[Role]
(
	[Id]							UNIQUEIDENTIFIER	NOT NULL,
	[Code]							NVARCHAR(10)		NOT NULL,
	[Name]							NVARCHAR(MAX)		NOT NULL,
	[Description]					NVARCHAR(MAX)		NOT NULL
)