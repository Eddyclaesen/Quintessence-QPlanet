﻿CREATE TABLE [dbo].[SimulationCombination] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [SimulationSetId]        UNIQUEIDENTIFIER NOT NULL,
    [SimulationDepartmentId] UNIQUEIDENTIFIER NULL,
    [SimulationLevelId]      UNIQUEIDENTIFIER NULL,
    [SimulationId]           UNIQUEIDENTIFIER NOT NULL,
    [Preparation]            INT              NOT NULL,
    [Execution]              INT              NOT NULL,
    [QCandidateLayout]       INT              DEFAULT(1)  NOT NULL,
    [Audit_CreatedBy]        NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]        DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]       NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]       DATETIME         NULL,
    [Audit_DeletedBy]        NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]        DATETIME         NULL,
    [Audit_IsDeleted]        BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_SimulationCombination] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SimulationCombination_Simulation] FOREIGN KEY ([SimulationId]) REFERENCES [dbo].[Simulation] ([Id]),
    CONSTRAINT [FK_SimulationCombination_SimulationDepartment] FOREIGN KEY ([SimulationDepartmentId]) REFERENCES [dbo].[SimulationDepartment] ([Id]),
    CONSTRAINT [FK_SimulationCombination_SimulationLevel] FOREIGN KEY ([SimulationLevelId]) REFERENCES [dbo].[SimulationLevel] ([Id]),
    CONSTRAINT [FK_SimulationCombination_SimulationSet] FOREIGN KEY ([SimulationSetId]) REFERENCES [dbo].[SimulationSet] ([Id])
);

