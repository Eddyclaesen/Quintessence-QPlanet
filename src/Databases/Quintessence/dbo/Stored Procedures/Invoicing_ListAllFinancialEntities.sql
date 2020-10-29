
CREATE PROCEDURE [dbo].Invoicing_ListAllFinancialEntities
AS
BEGIN
	SET NOCOUNT ON;

	select id, name, crmContactId, businessCentralCustomerId from dbo.Invoicing_FinancialEntity
END
