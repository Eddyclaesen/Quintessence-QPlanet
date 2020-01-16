CREATE TABLE [dbo].[ProjectComplaint] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [CrmProjectId]            INT              NOT NULL,
    [Subject]                 NVARCHAR (MAX)   NULL,
    [ComplaintDate]           DATETIME         NULL,
    [ComplaintDetails]        NVARCHAR (MAX)   NULL,
    [ComplaintStatusTypeId]   INT              NOT NULL,
    [ComplaintSeverityTypeId] INT              NOT NULL,
    [ComplaintTypeId]         INT              NOT NULL,
    [FollowUp]                NVARCHAR (MAX)   NULL,
    [Audit_CreatedBy]         NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]         DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]        NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]        DATETIME         NULL,
    [Audit_DeletedBy]         NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]         DATETIME         NULL,
    [Audit_IsDeleted]         BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SubmitterId]             UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProjectComplaint] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectComplaint_ComplaintSeverityType] FOREIGN KEY ([ComplaintSeverityTypeId]) REFERENCES [dbo].[ComplaintSeverityType] ([Id]),
    CONSTRAINT [FK_ProjectComplaint_ComplaintStatusType] FOREIGN KEY ([ComplaintStatusTypeId]) REFERENCES [dbo].[ComplaintStatusType] ([Id]),
    CONSTRAINT [FK_ProjectComplaint_ComplaintType] FOREIGN KEY ([ComplaintTypeId]) REFERENCES [dbo].[ComplaintType] ([Id]),
    CONSTRAINT [FK_ProjectComplaint_SubmitterId] FOREIGN KEY ([SubmitterId]) REFERENCES [dbo].[User] ([Id])
);



