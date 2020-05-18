CREATE PROCEDURE [dbo].[ListEvaluationFormMailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))

	/*Increase performance by putting record in temporary table*/
	SELECT	[EvaluationFormView].[FirstName]			AS [FirstName],
			[EvaluationFormView].[LastName]				AS [LastName],
			[EvaluationFormView].[Gender]				AS [Gender],
			[EvaluationFormView].[LanguageId]			AS [LanguageId],
			[EvaluationFormView].[VerificationCode]		AS [LoginCode],
			[CrmAssociateView].FirstName + ' ' + [CrmAssociateView].LastName AS [Proma]
	INTO #TempTable 
	FROM [dbo].[EvaluationFormView]
	LEFT JOIN CrmProjectView on [EvaluationFormView].CrmProjectId = CrmProjectView.Id
	LEFT JOIN CrmAssociateView on CrmProjectView.AssociateId = CrmAssociateView.Id
	WHERE [EvaluationFormView].[Id] = @Id
 
	/*Insert into key-value table*/
	INSERT INTO @Table
	SELECT 'GENDER', [dbo].[GenderToSalutation]([Gender], [LanguageId]) 
	FROM #TempTable

	INSERT INTO @Table
	SELECT 'PROMA', [Proma]
	FROM #TempTable
	
	INSERT INTO @Table
	SELECT 'FIRSTNAME', [FirstName] 
	FROM #TempTable
	
	INSERT INTO @Table
	SELECT 'LASTNAME', [LastName] 
	FROM #TempTable
	
	INSERT INTO @Table
	SELECT 'LOGINCODE', [LoginCode]
	FROM #TempTable

	DROP TABLE #TempTable

	SELECT *
	FROM @Table
END