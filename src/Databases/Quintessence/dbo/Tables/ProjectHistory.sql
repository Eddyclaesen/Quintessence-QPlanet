CREATE TABLE [dbo].[ProjectHistory] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [Name]                  NVARCHAR (100)   NOT NULL,
    [ProjectTypeId]         UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [ContactId]             INT              NOT NULL,
    [ProjectManagerId]      UNIQUEIDENTIFIER NULL,
    [CustomerAssistantId]   UNIQUEIDENTIFIER NULL,
    [StatusCode]            INT              NOT NULL,
    [Remarks]               TEXT             NULL,
    [FunctionTitle]         NVARCHAR (MAX)   NULL,
    [FunctionInformation]   TEXT             NULL,
    [DepartmentInformation] TEXT             NULL,
    [CrmProjectId]          INT              NULL,
    [DictionaryId]          UNIQUEIDENTIFIER NULL,
    [ActId]                 INT              NOT NULL,
    [CreatedOn]             DATETIME         NOT NULL
);

