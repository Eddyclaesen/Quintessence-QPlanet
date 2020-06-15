CREATE VIEW [dbo].[ProjectDna2CrmPersonView] AS
	SELECT		[ProjectDna2CrmPerson].*

	FROM		[ProjectDna2CrmPerson]	WITH (NOLOCK)

	WHERE		[ProjectDna2CrmPerson].[Audit_IsDeleted] = 0