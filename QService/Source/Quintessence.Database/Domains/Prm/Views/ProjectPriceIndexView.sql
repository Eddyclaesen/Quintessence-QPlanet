CREATE VIEW [dbo].[ProjectPriceIndexView] AS
	SELECT		*

	FROM		[ProjectPriceIndex]	WITH (NOLOCK)

	WHERE		[Audit_IsDeleted] = 0
