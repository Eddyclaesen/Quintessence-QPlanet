CREATE TABLE [dbo].[SimulationTranslation] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [SimulationId]     UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]       INT              NOT NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_SimulationTranslation] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SimulationTranslation_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_SimulationTranslation_Simulation] FOREIGN KEY ([SimulationId]) REFERENCES [dbo].[Simulation] ([Id]),
    CONSTRAINT [UK_SimulationTranslation_Simulation_Language] UNIQUE NONCLUSTERED ([SimulationId] ASC, [LanguageId] ASC)
);

