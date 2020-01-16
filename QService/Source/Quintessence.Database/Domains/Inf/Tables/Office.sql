CREATE TABLE [dbo].[Office]
(
	[Id]		INT				IDENTITY (1, 1) NOT NULL, 
    [ShortName] NVARCHAR(2)						NOT NULL, 
    [FullName]	NVARCHAR(MAX)					NOT NULL
)
