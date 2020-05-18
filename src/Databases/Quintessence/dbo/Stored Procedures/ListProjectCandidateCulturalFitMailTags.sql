CREATE PROCEDURE [dbo].[ListProjectCandidateCulturalFitMailTags]
	@Id			UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @Table TABLE([Tag] NVARCHAR(MAX), [Value] NVARCHAR(MAX))

	/*Increase performance by putting record in temporary table*/
	SELECT	[TheoremListRequestView].[VerificationCode]		AS [LoginCode],
			[TheoremListRequestView].[Deadline]				AS [Deadline]
	INTO #TempTable 
	FROM [dbo].[TheoremListRequestView]
	INNER JOIN [ProjectCandidateView]
		ON [ProjectCandidateView].[CandidateId] = [TheoremListRequestView].[CandidateId] 
		AND [ProjectCandidateView].[ProjectId] = [TheoremListRequestView].[ProjectId]
		AND [ProjectCandidateView].[Id] = @Id
 
	/*Insert into key-value table*/
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