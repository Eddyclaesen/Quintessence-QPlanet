CREATE TABLE [dbo].[Candidate] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [FirstName]        NVARCHAR (MAX)   NOT NULL,
    [LastName]         NVARCHAR (MAX)   NOT NULL,
    [Email]            NVARCHAR (MAX)   NULL,
    [Gender]           CHAR (1)         NOT NULL,
    [LanguageId]       INT              NOT NULL,
    [Audit_CreatedBy]  NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]  DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy] NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn] DATETIME         NULL,
    [Audit_DeletedBy]  NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]  DATETIME         NULL,
    [Audit_IsDeleted]  BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [LegacyId]         INT              NULL,
    [Phone]            NVARCHAR (MAX)   NULL,
    [Reference]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER dbo.Candidate_Updated
   ON  [dbo].[Candidate]
   AFTER UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Codes TABLE (Code NVARCHAR(MAX))
	
	DECLARE @CandidateId NVARCHAR(MAX)
	DECLARE @LanguageId INT
	SET @CandidateId = (SELECT Id from inserted)
	SET @LanguageId = (SELECT LanguageId from inserted)

    DECLARE @ProjectCandidates TABLE (Id NVARCHAR(MAX))
	insert into @ProjectCandidates
	select Id
	from ProjectCandidateView 
	where CandidateId = @CandidateId

	IF EXISTS (select * 
				from [dbo].[ProjectCandidateCategoryDetailType3View]
				where ProjectCandidateId IN (select Id from @ProjectCandidates))
		begin
			insert into @Codes
			select LoginCode
			from [dbo].[ProjectCandidateCategoryDetailType3View]
			where ProjectCandidateId IN (select Id from @ProjectCandidates)

			update qata.dbo.candidate
			set LanguageID = CASE @LanguageId
									WHEN 1 THEN 'NL' COLLATE DATABASE_DEFAULT
									WHEN 2 THEN 'FR' COLLATE DATABASE_DEFAULT
									ELSE 'EN' COLLATE DATABASE_DEFAULT
									END
			where CandidateID in (select candidateid from qata.dbo.session where code in (select Code COLLATE DATABASE_DEFAULT from @Codes))

		end
END
