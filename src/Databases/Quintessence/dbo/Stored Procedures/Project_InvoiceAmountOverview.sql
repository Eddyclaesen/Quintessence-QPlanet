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
			AND			[ProjectCandidateView].[InvoicedDate] IS NOT NULL

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
			AND			[ProjectCandidateCategoryDetailType1View].[InvoicedDate] IS NOT NULL

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
			AND			[ProjectCandidateCategoryDetailType2View].[InvoicedDate] IS NOT NULL
	
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
			AND			[ProjectCandidateCategoryDetailType3View].[InvoicedDate] IS NOT NULL

		/*ProjectFixedPrice*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectFixedPriceView].[Amount],
						[ProjectFixedPriceView].[InvoicedDate]
		FROM			[ProjectFixedPriceView]
		WHERE			[ProjectFixedPriceView].[ProjectId] = @ProjectId
			AND			[ProjectFixedPriceView].[InvoiceStatusCode] = 100 --Invoiced
			AND			[ProjectFixedPriceView].[InvoicedDate] IS NOT NULL

		/*ProjectProduct*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProjectProductView].[InvoiceAmount],
						[ProjectProductView].[InvoicedDate]
		FROM			[ProjectProductView]
		WHERE			[ProjectProductView].[ProjectId] = @ProjectId
			AND			[ProjectProductView].[InvoiceStatusCode] = 100 --Invoiced
			AND			[ProjectProductView].[InvoicedDate] IS NOT NULL
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
			AND			[ProjectFixedPriceView].[InvoicedDate] IS NOT NULL

		/*TimesheetEntry*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[TimesheetEntryView].[InvoiceAmount],
						[TimesheetEntryView].[InvoicedDate]
		FROM			[TimesheetEntryView]
		WHERE			[TimesheetEntryView].[ProjectId] = @ProjectId
			AND			[TimesheetEntryView].[InvoiceStatusCode] = 100 --Invoiced
			AND			[TimesheetEntryView].[InvoicedDate] IS NOT NULL

		/*ProductsheetEntry*/
		INSERT INTO		@InvoiceAmountTable
		SELECT			@ProjectId,
						[ProductsheetEntryView].[InvoiceAmount],
						[ProductsheetEntryView].[InvoicedDate]
		FROM			[ProductsheetEntryView]
		WHERE			[ProductsheetEntryView].[ProjectId] = @ProjectId
			AND			[ProductsheetEntryView].[InvoiceStatusCode] = 100 --Invoiced
			AND			[ProductsheetEntryView].[InvoicedDate] IS NOT NULL
	END

	SELECT	*
	FROM	@InvoiceAmountTable
END