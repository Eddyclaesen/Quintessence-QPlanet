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
               0,
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
CREATE UNIQUE CLUSTERED INDEX [IX_SimulationCombinationMemos_SimulationCombinationId_Position]
    ON [dbo].[SimulationCombinationMemos]([SimulationCombinationId] ASC, [Position] ASC);

GO
PRINT N'Creating [dbo].[FK_SimulationCombinationMemos_Simulation]...';


GO
ALTER TABLE [dbo].[SimulationCombinationMemos] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombinationMemos_SimulationCombination] FOREIGN KEY ([SimulationCombinationId]) REFERENCES [dbo].[SimulationCombination] ([Id]);


GO
PRINT N'Creating [dbo].[SimulationCombinationMemoTranslations]...';


GO
CREATE TABLE [dbo].[SimulationCombinationMemoTranslations] (
    [Id]                          UNIQUEIDENTIFIER NOT NULL,
    [SimulationCombinationMemoId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]                  INT              NOT NULL,
    [Title]                       NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_SimulationCombinationMemoTranslations] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_SimulationCombinationMemoTranslations_SimulationCombinationMemos]...';


GO
ALTER TABLE [dbo].[SimulationCombinationMemoTranslations] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombinationMemoTranslations_SimulationCombinationMemos] FOREIGN KEY ([SimulationCombinationMemoId]) REFERENCES [dbo].[SimulationCombinationMemos] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SimulationCombinationMemoTranslations_Language]...';


GO
ALTER TABLE [dbo].[SimulationCombinationMemoTranslations] WITH NOCHECK
    ADD CONSTRAINT [FK_SimulationCombinationMemoTranslations_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]);
GO

GO
CREATE PROCEDURE [QCandidate].[Assessment_GetByCandidateIdAndDateAndLanguage]
	@candidateId UNIQUEIDENTIFIER,
	@date DATE,
	@language char(2)
AS

	SET NOCOUNT ON;
	DECLARE @LanguageId int
	SET @LanguageId = case @language
		when 'nl' then 1
		when 'fr' then 2
		else 3
	end

SELECT
	--Customer
		cc.Id,
		cc.Name,

	--Position
		p.Id,
		p.Name,

	--DayProgram
		@date AS Date,
		--Location
			o.Id,
			o.FullName AS Name,
		--ProgramComponents
			prc.Id,
			prc.[Start],
			prc.[End],
			CASE 
					WHEN (uLeadAssess.FirstName is null and sc.Preparation > 0) THEN s.[Name]+ CASE @LanguageId 
																									WHEN 1 THEN ' (voorbereiding)'
																									WHEN 2 THEN ' (préparation)'
																									ELSE ' (preparation)'
																									END
					WHEN (uLeadAssess.FirstName is not null and sc.Execution > 0) THEN s.[Name]+ CASE @LanguageId
																									WHEN 1 THEN ' (uitvoering)'
																									WHEN 2 THEN ' (execution)'
																									ELSE ' (execution)'
																									END
					ELSE s.[Name]
					END AS [Name],
			prc.Description,
			prc.SimulationCombinationId,
			ISNULL(sc.QCandidateLayoutId, 0) AS QCandidateLayoutId,
			--Room
				ar.Id,
				ar.[Name],
			--LeadAssessor
				uLeadAssess.Id,
				uLeadAssess.FirstName,
				uLeadAssess.LastName,
			--CoAssessor
				uCoAssess.Id,
				uCoAssess.FirstName,
				uCoAssess.LastName
				--sc.Preparation,
				--sc.Execution		
FROM
	dbo.candidate c WITH (NOLOCK)
	INNER JOIN dbo.ProjectCandidate pc WITH (NOLOCK)
		ON pc.CandidateId = c.Id
	INNER JOIN dbo.Project p WITH (NOLOCK)
		ON p.Id = pc.ProjectId
	INNER JOIN dbo.CrmContact cc WITH (NOLOCK)
		ON cc.Id = p.ContactId
	INNER JOIN dbo.ProgramComponent prc WITH (NOLOCK)
		ON prc.ProjectCandidateId = pc.Id
	LEFT OUTER JOIN dbo.[User] uLeadAssess WITH (NOLOCK)
		ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT OUTER JOIN dbo.[User] uCoAssess WITH (NOLOCK)
		ON uCoAssess.Id = prc.CoAssessorUserId
	LEFT OUTER JOIN (dbo.SimulationCombination sc WITH (NOLOCK)
		INNER JOIN dbo.SimulationTranslationView s WITH (NOLOCK)
			ON s.SimulationId = sc.SimulationId)
		ON sc.Id = prc.SimulationCombinationId	
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK)
		ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK)
		ON o.Id = ar.OfficeId
WHERE
	c.Id = @candidateId
	AND CONVERT(DATE, prc.Start) = @date
	AND ISNULL(prc.Description,'') NOT LIKE '%Input scoring%'
	AND CONVERT(VARCHAR, ISNULL(prc.Description,'')) NOT IN ('Preparation consultant','Assessor debriefing','Proma','Assessor debriefing GGI')
	AND ISNULL(s.LanguageId, @LanguageId) = @LanguageId
	AND prc.Audit_IsDeleted = 0
ORDER BY
	prc.Start,
	prc.[End]