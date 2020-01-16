INSERT INTO [Project2CrmProject]
	SELECT	[Id], [CrmProjectId]
	FROM	[ProjectHistory]
	WHERE	[CrmProjectId] != 0