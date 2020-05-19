CREATE PROCEDURE Project_ListRecentProjects
	@UserId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProjectView].[Id] AS ProjectId,
				[ProjectView].[Name] AS ProjectName,
				[CrmContact].[name] + ' ' + [CrmContact].[department] AS ContactName,
				[ProjectTypeView].[Name] AS ProjectTypeName
	
	FROM		[ProjectView]
	
	INNER JOIN	[ProjectTypeView]
		ON		[ProjectTypeView].[Id] = [ProjectView].[ProjectTypeId]
		
	INNER JOIN	[CrmContact]
		ON		[CrmContact].[Id] = [ProjectView].[ContactId]

	WHERE		[ProjectView].[ProjectManagerId] = @UserId
	
	ORDER BY	[ProjectView].[Audit_CreatedOn] DESC
END