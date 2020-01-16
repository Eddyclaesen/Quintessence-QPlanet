CREATE VIEW [dbo].[ProjectFixedPriceView] AS
	SELECT		*

	FROM		[ProjectFixedPrice]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0
