CREATE VIEW [dbo].[ProjectDnaTypeView] AS
	SELECT		[ProjectDnaType].*

	FROM		[ProjectDnaType]	WITH (NOLOCK)

	WHERE		[ProjectDnaType].[Audit_IsDeleted] = 0