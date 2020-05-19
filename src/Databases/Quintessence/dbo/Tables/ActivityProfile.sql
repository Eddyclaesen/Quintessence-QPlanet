CREATE TABLE [dbo].[ActivityProfile] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ActivityId]         UNIQUEIDENTIFIER NOT NULL,
    [ProfileId]          UNIQUEIDENTIFIER NOT NULL,
    [DayRate]            DECIMAL (18, 2)  NOT NULL,
    [HalfDayRate]        DECIMAL (18, 2)  NOT NULL,
    [HourlyRate]         DECIMAL (18, 2)  NOT NULL,
    [IsolatedHourlyRate] DECIMAL (18, 2)  NOT NULL,
    [Audit_CreatedBy]    NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]    DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]   NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]   DATETIME         NULL,
    [Audit_DeletedBy]    NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]    DATETIME         NULL,
    [Audit_IsDeleted]    BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_ActivityProfile] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityProfile_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity] ([Id]),
    CONSTRAINT [FK_ActivityProfile_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([Id])
);

