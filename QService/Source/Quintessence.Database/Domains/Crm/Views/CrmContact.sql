CREATE VIEW [dbo].[CrmContact]
AS
SELECT		*
FROM		[CrmReplicationContact]	WITH (NOLOCK)