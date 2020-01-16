CREATE TABLE [dbo].[ProjectType2ProjectTypeCategory](
	[ProjectTypeId]				UNIQUEIDENTIFIER NOT NULL,
	[ProjectTypeCategoryId]		UNIQUEIDENTIFIER NOT NULL,
	[IsMain]					BIT		DEFAULT 0,
)