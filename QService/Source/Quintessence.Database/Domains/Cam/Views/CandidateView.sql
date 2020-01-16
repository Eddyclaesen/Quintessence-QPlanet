CREATE VIEW [dbo].[CandidateView] AS
	SELECT		c.*, 
				lv.Name		AS LanguageName
	FROM		[dbo].[Candidate] c	WITH (NOLOCK)
	INNER JOIN	[dbo].[LanguageView] lv
		ON		c.LanguageId = lv.Id
	WHERE		c.[Audit_IsDeleted] = 0
