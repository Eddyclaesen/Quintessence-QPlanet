
--Re-initialise views, unchanged, so FinancialEntityId-column is picked up from referenced table
CREATE VIEW [dbo].[ProjectFixedPriceView] AS
	SELECT		*
	FROM		[ProjectFixedPrice]	WITH (NOLOCK)
	WHERE		[Audit_IsDeleted] = 0
