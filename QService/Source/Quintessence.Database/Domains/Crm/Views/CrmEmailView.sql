CREATE VIEW [dbo].[CrmEmailView]
	AS 
	SELECT		*
	FROM		[CrmReplicationEmail]	WITH (NOLOCK)