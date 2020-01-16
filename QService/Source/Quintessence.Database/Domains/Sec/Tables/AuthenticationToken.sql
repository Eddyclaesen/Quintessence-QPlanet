CREATE TABLE [dbo].[AuthenticationToken]
(
	[Id]							UNIQUEIDENTIFIER	NOT NULL,
	[UserId]						UNIQUEIDENTIFIER	NOT NULL,
	[ValidFrom]						DATETIME			NOT NULL,
	[ValidTo]						DATETIME			NOT NULL,
	[IssuedOn]						DATETIME			NOT NULL
)