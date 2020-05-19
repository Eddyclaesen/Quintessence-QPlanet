CREATE TABLE [dbo].[ProjectRevenueDistribution] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]        UNIQUEIDENTIFIER NOT NULL,
    [CrmProjectId]     INT              NOT NULL,
    [Revenue]          DECIMAL (18, 2)  NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ProjectRevenueDistribution] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProjectRevenueDistribution_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [UK_ProjectRevenueDistribution] UNIQUE NONCLUSTERED ([ProjectId] ASC, [CrmProjectId] ASC)
);

