CREATE VIEW [dbo].[ContactDetailView] AS
	SELECT		*
	FROM		[ContactDetail]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0