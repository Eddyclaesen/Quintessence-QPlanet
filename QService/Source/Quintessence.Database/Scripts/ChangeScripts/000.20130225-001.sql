/*Add new mail template for customer cultural fit invitation*/
DECLARE @MailTemplateId UNIQUEIDENTIFIER = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'CustomerCulturalFitInvitation', 'CUSTCULTFIT', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListCulturalFitMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Uitnodiging', '', 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Invitation', '', 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Invitation', '', 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Einladung', '', 4, @MailTemplateId)

/*Update MailTemplate integrated cultural fit invitation mail*/
UPDATE	[MailTemplate]
SET		[MailTemplate].[Name] = 'CandidateCulturalFitInvitation',
		[MailTemplate].[Code] = 'CANDCULTFIT'
WHERE	[MailTemplate].[Code] = 'CULTFIT'

CREATE TABLE [dbo].[Setting] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Key]   NVARCHAR (255) NOT NULL,
    [Value] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED ([Id] ASC)
);

/*Cultural fit settings*/
IF NOT EXISTS(SELECT * FROM [Setting] WHERE [Key] = 'mailserver')
BEGIN
	INSERT INTO [Setting]([Key], Value) VALUES('mailserver', 'vm-quintmail')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE [Key] = 'mailfrom')
BEGIN
	INSERT INTO [Setting]([Key], Value) VALUES('mailfrom', 'secretariaat@quintessence.be')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE [Key] = 'siteUrl')
BEGIN
	INSERT INTO [Setting]([Key], Value) VALUES('siteUrl', 'http://culturalfit.myquintessence.be/site/{0}/Login/Index')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE [Key] = 'companyName')
BEGIN
	INSERT INTO [Setting]([Key], Value) VALUES('companyName', 'Quintessence Consulting NV')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE [Key] = 'mailCc')
BEGIN
	INSERT INTO [Setting]([Key], Value) VALUES('mailCc', 'secretariaat@quintessence.be')
END

GO
PRINT N'Creating [dbo].[CulturalFitAssociateView]...';


GO
CREATE VIEW [dbo].[CulturalFitAssociateView] AS
	SELECT	[CrmReplicationAssociate].[Id]			AS [Id],
			[CrmReplicationAssociate].[UserName]	AS [UserName],
			[CrmReplicationAssociate].[FirstName]	AS [FirstName],
			[CrmReplicationAssociate].[LastName]	AS [LastName],
			[CrmUserEmailView].[Address]			AS [Email],
			[UserView].[Id]							AS [UserId]
	FROM [CrmReplicationAssociate]
	INNER JOIN [UserView] 
		ON [UserView].[AssociateId] =  [CrmReplicationAssociate].[Id]
	INNER JOIN [CrmUserEmailView]
		ON [CrmUserEmailView].[UserId] = [UserView].[Id]
GO
PRINT N'Creating [dbo].[ListCulturalFitMailTags]...';


GO
CREATE PROCEDURE [dbo].[ListCulturalFitMailTags]
	@Id					UNIQUEIDENTIFIER,
	@LanguageId			INT
AS
BEGIN
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))

	/*Increase performance by putting record in temporary table*/
	SELECT	[CrmEmailView].[FirstName]						AS [FirstName],
			[CrmEmailView].[LastName]						AS [LastName],
			[CrmEmailView].[ContactName]					AS [ContactName],
			[TheoremListRequestView].[VerificationCode]		AS [LoginCode],
			[TheoremListRequestView].[Deadline]				AS [Deadline]
	INTO #TempTable 
	FROM [dbo].[TheoremListRequestView]
	INNER JOIN [dbo].[CrmEmailView]
		ON [CrmEmailView].[Id] = [TheoremListRequestView].[CrmEmailId]
	WHERE [TheoremListRequestView].[Id] = @Id
 
	/*Insert into key-value table*/
	INSERT INTO @Table
	SELECT 'GENDER', [dbo].[GenderToSalutation] (Gender, @LanguageId)
	FROM #TempTable
	
	INSERT INTO @Table
	SELECT 'FIRSTNAME', [FirstName]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'LASTNAME', [LastName]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'CONTACT', [ContactName]
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'LOGINCODE', [LoginCode] 
	FROM #TempTable
	
	INSERT INTO @Table
	SELECT 'DEADLINE', CONVERT(VARCHAR(10), [Deadline] , 103)
	FROM #TempTable

	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END
GO
PRINT N'Creating [dbo].[Project_InvoiceAmountOverview]...';


GO
CREATE PROCEDURE [dbo].[Project_InvoiceAmountOverview]
	@ProjectId				UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @InvoiceAmountTable AS TABLE(ProjectId UNIQUEIDENTIFIER, InvoiceAmount DECIMAL(18,2), InvoicedDate DATETIME)
	
	/*ACDC*/
	IF(EXISTS(SELECT * FROM [AssessmentDevelopmentProjectView] WHERE [Id] = @ProjectId))
	BEGIN
		/*ProjectCandidate*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectCandidateView].[InvoiceAmount],
						[ProjectCandidateView].[InvoicedDate]
		FROM			[ProjectCandidateView]
		WHERE			[ProjectCandidateView].[ProjectId] = @ProjectId
			AND			[ProjectCandidateView].[InvoiceStatusCode] = 100 --Invoiced

		/*ProjectCandidateCategoryDetailType1*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectCandidateCategoryDetailType1View].[InvoiceAmount],
						[ProjectCandidateCategoryDetailType1View].[InvoicedDate]
		FROM			[ProjectCandidateCategoryDetailType1View]
		INNER JOIN		[ProjectCandidateView]
			ON			[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType1View].[ProjectCandidateId]
			AND			[ProjectCandidateView].[ProjectId] = @ProjectId
		WHERE			[ProjectCandidateCategoryDetailType1View].[InvoiceStatusCode] = 100 --Invoiced

		/*ProjectCandidateCategoryDetailType2*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectCandidateCategoryDetailType2View].[InvoiceAmount],
						[ProjectCandidateCategoryDetailType2View].[InvoicedDate]
		FROM			[ProjectCandidateCategoryDetailType2View]
		INNER JOIN		[ProjectCandidateView]
			ON			[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType2View].[ProjectCandidateId]
			AND			[ProjectCandidateView].[ProjectId] = @ProjectId
		WHERE			[ProjectCandidateCategoryDetailType2View].[InvoiceStatusCode] = 100 --Invoiced
	
		/*ProjectCandidateCategoryDetailType3*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectCandidateCategoryDetailType3View].[InvoiceAmount],
						[ProjectCandidateCategoryDetailType3View].[InvoicedDate]
		FROM			[ProjectCandidateCategoryDetailType3View]
		INNER JOIN		[ProjectCandidateView]
			ON			[ProjectCandidateView].[Id] = [ProjectCandidateCategoryDetailType3View].[ProjectCandidateId]
			AND			[ProjectCandidateView].[ProjectId] = @ProjectId
		WHERE			[ProjectCandidateCategoryDetailType3View].[InvoiceStatusCode] = 100 --Invoiced

		/*ProjectFixedPrice*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectFixedPriceView].[Amount],
						[ProjectFixedPriceView].[InvoicedDate]
		FROM			[ProjectFixedPriceView]
		WHERE			[ProjectFixedPriceView].[ProjectId] = @ProjectId
			AND			[ProjectFixedPriceView].[InvoiceStatusCode] = 100 --Invoiced

		/*ProjectProduct*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectProductView].[InvoiceAmount],
						[ProjectProductView].[InvoicedDate]
		FROM			[ProjectProductView]
		WHERE			[ProjectProductView].[ProjectId] = @ProjectId
			AND			[ProjectProductView].[InvoiceStatusCode] = 100 --Invoiced
	END
	
	/*Consultancy*/
	IF(EXISTS(SELECT * FROM [ConsultancyProjectView] WHERE [Id] = @ProjectId))
	BEGIN
		/*ProjectFixedPrice*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectFixedPriceView].[Amount],
						[ProjectFixedPriceView].[InvoicedDate]
		FROM			[ProjectFixedPriceView]
		WHERE			[ProjectFixedPriceView].[ProjectId] = @ProjectId
			AND			[ProjectFixedPriceView].[InvoiceStatusCode] = 100 --Invoiced

		/*TimesheetEntry*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[TimesheetEntryView].[InvoiceAmount],
						[TimesheetEntryView].[InvoicedDate]
		FROM			[TimesheetEntryView]
		WHERE			[TimesheetEntryView].[ProjectId] = @ProjectId
			AND			[TimesheetEntryView].[InvoiceStatusCode] = 100 --Invoiced

		/*ProductsheetEntry*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProductsheetEntryView].[InvoiceAmount],
						[ProductsheetEntryView].[InvoicedDate]
		FROM			[ProductsheetEntryView]
		WHERE			[ProductsheetEntryView].[ProjectId] = @ProjectId
			AND			[ProductsheetEntryView].[InvoiceStatusCode] = 100 --Invoiced
	END

	SELECT	*
	FROM	@InvoiceAmountTable
END
GO

