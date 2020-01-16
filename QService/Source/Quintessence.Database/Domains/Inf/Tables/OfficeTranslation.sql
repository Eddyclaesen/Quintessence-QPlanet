CREATE TABLE [dbo].[OfficeTranslation]
(
	[Id]			INT				IDENTITY (1, 1) NOT NULL,
	[OfficeId]		INT								NOT NULL, 
    [LanguageId]	INT								NOT NULL, 
    [Name]			NVARCHAR(MAX)					NOT NULL
)
