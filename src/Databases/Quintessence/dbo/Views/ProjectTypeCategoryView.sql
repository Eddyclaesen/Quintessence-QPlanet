CREATE VIEW [dbo].[ProjectTypeCategoryView] AS
	SELECT		[ProjectTypeCategory].*,
				[ProjectType2ProjectTypeCategory].[ProjectTypeId],
				[ProjectType2ProjectTypeCategory].[IsMain]
				
	FROM		[ProjectTypeCategory]	WITH (NOLOCK)
	
	INNER JOIN	[ProjectType2ProjectTypeCategory]	WITH (NOLOCK)
		ON		[ProjectType2ProjectTypeCategory].[ProjectTypeCategoryId] = [ProjectTypeCategory].[Id]

	WHERE		[Audit_IsDeleted] = 0