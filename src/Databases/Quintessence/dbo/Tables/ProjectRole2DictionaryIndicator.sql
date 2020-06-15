CREATE TABLE [dbo].[ProjectRole2DictionaryIndicator] (
    [ProjectRoleId]         UNIQUEIDENTIFIER NOT NULL,
    [DictionaryIndicatorId] UNIQUEIDENTIFIER NOT NULL,
    [Norm]                  INT              DEFAULT ((10)) NOT NULL
);

