CREATE VIEW [dbo].[ProjectRoleView] AS
	SELECT		pr.*, cc.name AS ContactName
	FROM		[ProjectRole] pr	WITH (NOLOCK)
	LEFT JOIN	[CrmContact] cc
		ON		cc.Id = pr.ContactId
 	WHERE		[Audit_IsDeleted] = 0
