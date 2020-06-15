CREATE TABLE [dbo].[ActivityType2Profile] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ActivityTypeId]     UNIQUEIDENTIFIER NOT NULL,
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
    CONSTRAINT [PK_ActivityType2Profile] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ActivityType2Profile_ActivityType] FOREIGN KEY ([ActivityTypeId]) REFERENCES [dbo].[ActivityType] ([Id]),
    CONSTRAINT [FK_ActivityType2Profile_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([Id])
);

