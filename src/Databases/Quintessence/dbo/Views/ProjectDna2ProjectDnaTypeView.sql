CREATE VIEW [dbo].[ProjectDna2ProjectDnaTypeView] AS
	SELECT		[ProjectDna2ProjectDnaType].*

	FROM		[ProjectDna2ProjectDnaType]	WITH (NOLOCK)

	WHERE		[ProjectDna2ProjectDnaType].[Audit_IsDeleted] = 0