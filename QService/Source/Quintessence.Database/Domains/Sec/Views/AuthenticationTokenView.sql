CREATE VIEW [dbo].[AuthenticationTokenView]	AS 
	SELECT		* 
	FROM		[AuthenticationToken]	WITH (NOLOCK)
	WHERE		[ValidTo] > GETDATE()
