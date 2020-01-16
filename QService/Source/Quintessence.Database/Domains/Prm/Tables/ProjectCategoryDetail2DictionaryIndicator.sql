CREATE TABLE [dbo].[ProjectCategoryDetail2DictionaryIndicator](
	[Id]							UNIQUEIDENTIFIER	NOT NULL,
	[ProjectCategoryDetailId]		UNIQUEIDENTIFIER	NOT NULL,
	[DictionaryIndicatorId]			UNIQUEIDENTIFIER	NOT NULL,
	[IsDefinedByRole]				BIT					NOT NULL		DEFAULT 0,
	[IsStandard]					BIT					NULL,
	[IsDistinctive]					BIT					NULL
)