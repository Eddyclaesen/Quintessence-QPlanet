CREATE TABLE [dbo].[TheoremListRequest] (
    [Id]                       UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]                UNIQUEIDENTIFIER NOT NULL,
    [ContactId]                INT              NOT NULL,
    [CrmEmailId]               INT              NULL,
    [CandidateId]              UNIQUEIDENTIFIER NULL,
    [RequestDate]              DATETIME         NOT NULL,
    [Deadline]                 DATETIME         NOT NULL,
    [TheoremListRequestTypeId] INT              NOT NULL,
    [VerificationCode]         VARCHAR (6)      NOT NULL,
    [IsMailSent]               BIT              NOT NULL,
    [Audit_CreatedBy]          NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]          DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]         NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]         DATETIME         NULL,
    [Audit_DeletedBy]          NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]          DATETIME         NULL,
    [Audit_IsDeleted]          BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Description]              NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_TheoremListRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TheoremListRequest_TheoremListRequestType] FOREIGN KEY ([TheoremListRequestTypeId]) REFERENCES [dbo].[TheoremListRequestType] ([Id])
);

