CREATE TABLE [dbo].[ProjectPlanPhaseEntry] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ProjectPlanPhaseId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]           DECIMAL (18, 2)  NOT NULL,
    [Deadline]           DATETIME         NOT NULL,
    [Audit_CreatedBy]    NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]    DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]   NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]   DATETIME         NULL,
    [Audit_DeletedBy]    NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]    DATETIME         NULL,
    [Audit_IsDeleted]    BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectPlanPhaseEntry] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectPlanPhaseEntry_ProjectPlanPhase] FOREIGN KEY ([ProjectPlanPhaseId]) REFERENCES [dbo].[ProjectPlanPhase] ([Id])
);

