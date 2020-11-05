GO
PRINT N'Dropping unnamed constraint on [dbo].[SimulationCombination]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [DF__Simulatio__Audit__73DA2C14];


GO
PRINT N'Dropping unnamed constraint on [dbo].[SimulationCombination]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [DF__Simulatio__Audit__74CE504D];


GO
PRINT N'Dropping unnamed constraint on [dbo].[SimulationCombination]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [DF__Simulatio__Audit__75C27486];


GO
PRINT N'Dropping unnamed constraint on [dbo].[SimulationCombination]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [DF__Simulatio__Audit__76B698BF];


GO
PRINT N'Dropping [dbo].[FK_SimulationCombination2Language_Simulation]...';


GO
ALTER TABLE [dbo].[SimulationCombination2Language] DROP CONSTRAINT [FK_SimulationCombination2Language_Simulation];


GO
PRINT N'Dropping [dbo].[FK_ProjectCandidateCompetenceSimulationScore_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCandidateCompetenceSimulationScore] DROP CONSTRAINT [FK_ProjectCandidateCompetenceSimulationScore_SimulationCombination];


GO
PRINT N'Dropping [dbo].[FK_ProjectCandidateIndicatorSimulationScore_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCandidateIndicatorSimulationScore] DROP CONSTRAINT [FK_ProjectCandidateIndicatorSimulationScore_SimulationCombination];


GO
PRINT N'Dropping [dbo].[FK_ProjectCategoryDetail2C2C_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCategoryDetail2Competence2Combination] DROP CONSTRAINT [FK_ProjectCategoryDetail2C2C_SimulationCombination];


GO
PRINT N'Dropping [dbo].[FK_ProjectCategoryDetail2SimulationCombination_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCategoryDetail2SimulationCombination] DROP CONSTRAINT [FK_ProjectCategoryDetail2SimulationCombination_SimulationCombination];


GO
PRINT N'Dropping [dbo].[FK_SimulationCombination_SimulationDepartment]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [FK_SimulationCombination_SimulationDepartment];


GO
PRINT N'Dropping [dbo].[FK_SimulationCombination_Simulation]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [FK_SimulationCombination_Simulation];


GO
PRINT N'Dropping [dbo].[FK_SimulationCombination_SimulationLevel]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [FK_SimulationCombination_SimulationLevel];


GO
PRINT N'Dropping [dbo].[FK_SimulationCombination_SimulationSet]...';


GO
ALTER TABLE [dbo].[SimulationCombination] DROP CONSTRAINT [FK_SimulationCombination_SimulationSet];




GO
PRINT N'Starting rebuilding table [dbo].[SimulationCombination]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_SimulationCombination] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [SimulationSetId]        UNIQUEIDENTIFIER NOT NULL,
    [SimulationDepartmentId] UNIQUEIDENTIFIER NULL,
    [SimulationLevelId]      UNIQUEIDENTIFIER NULL,
    [SimulationId]           UNIQUEIDENTIFIER NOT NULL,
    [Preparation]            INT              NOT NULL,
    [Execution]              INT              NOT NULL,
    [QCandidateLayoutId]     INT              NOT NULL,
    [Audit_CreatedBy]        NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]        DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]       NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]       DATETIME         NULL,
    [Audit_DeletedBy]        NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]        DATETIME         NULL,
    [Audit_IsDeleted]        BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_SimulationCombination1] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[SimulationCombination])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_SimulationCombination] ([Id], [SimulationSetId], [SimulationDepartmentId], [SimulationLevelId], [SimulationId], [Preparation], [Execution], [QCandidateLayoutId], [Audit_CreatedBy], [Audit_CreatedOn], [Audit_ModifiedBy], [Audit_ModifiedOn], [Audit_DeletedBy], [Audit_DeletedOn], [Audit_IsDeleted], [Audit_VersionId])
        SELECT [Id],
               [SimulationSetId],
               [SimulationDepartmentId],
               [SimulationLevelId],
               [SimulationId],
               [Preparation],
               [Execution],
               1,
               [Audit_CreatedBy],
               [Audit_CreatedOn],
               [Audit_ModifiedBy],
               [Audit_ModifiedOn],
               [Audit_DeletedBy],
               [Audit_DeletedOn],
               [Audit_IsDeleted],
               [Audit_VersionId]
        FROM   [dbo].[SimulationCombination];
    END

DROP TABLE [dbo].[SimulationCombination];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_SimulationCombination]', N'SimulationCombination';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_SimulationCombination1]', N'PK_SimulationCombination', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_SimulationCombination2Language_Simulation]...';


GO
ALTER TABLE [dbo].[SimulationCombination2Language] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombination2Language_Simulation] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ProjectCandidateCompetenceSimulationScore_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCandidateCompetenceSimulationScore] WITH NOCHECK
    ADD CONSTRAINT [FK_ProjectCandidateCompetenceSimulationScore_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ProjectCandidateIndicatorSimulationScore_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCandidateIndicatorSimulationScore] WITH NOCHECK
    ADD CONSTRAINT [FK_ProjectCandidateIndicatorSimulationScore_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ProjectCategoryDetail2C2C_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCategoryDetail2Competence2Combination] WITH NOCHECK
    ADD CONSTRAINT [FK_ProjectCategoryDetail2C2C_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ProjectCategoryDetail2SimulationCombination_SimulationCombination]...';


GO
ALTER TABLE [dbo].[ProjectCategoryDetail2SimulationCombination] WITH NOCHECK
    ADD CONSTRAINT [FK_ProjectCategoryDetail2SimulationCombination_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SimulationCombination_SimulationDepartment]...';


GO
ALTER TABLE [dbo].[SimulationCombination] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombination_SimulationDepartment] FOREIGN KEY ([SimulationDepartmentId]) REFERENCES [dbo].[SimulationDepartment] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SimulationCombination_Simulation]...';


GO
ALTER TABLE [dbo].[SimulationCombination] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombination_Simulation] FOREIGN KEY ([SimulationId]) REFERENCES [dbo].[Simulation] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SimulationCombination_SimulationLevel]...';


GO
ALTER TABLE [dbo].[SimulationCombination] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombination_SimulationLevel] FOREIGN KEY ([SimulationLevelId]) REFERENCES [dbo].[SimulationLevel] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SimulationCombination_SimulationSet]...';


GO
ALTER TABLE [dbo].[SimulationCombination] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombination_SimulationSet] FOREIGN KEY ([SimulationSetId]) REFERENCES [dbo].[SimulationSet] ([Id]);


exec sp_refreshview [dbo.SimulationCombinationView];

exec sp_refreshview [dbo.SimulationMatrixEntryView];


GO
PRINT N'Creating [dbo].[SimulationCombinationMemos]...';


GO
CREATE TABLE [dbo].[SimulationCombinationMemos] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [SimulationCombinationId] UNIQUEIDENTIFIER NOT NULL,
    [Position]                INT              NOT NULL,
    CONSTRAINT [PK_SimulationCombinationMemos] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[SimulationCombinationMemos].[IX_SimulationCombination_Position]...';


GO
CREATE CLUSTERED INDEX [IX_SimulationCombinationMemos_SimulationCombinationId_Position]
    ON [dbo].[SimulationCombinationMemos]([SimulationCombinationId] ASC, [Position] ASC);

GO
PRINT N'Creating [dbo].[FK_SimulationCombinationMemos_Simulation]...';


GO
ALTER TABLE [dbo].[SimulationCombinationMemos] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombinationMemos_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);

