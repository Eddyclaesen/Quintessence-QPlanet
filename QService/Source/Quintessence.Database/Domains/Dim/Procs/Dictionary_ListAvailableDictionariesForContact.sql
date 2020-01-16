CREATE PROCEDURE Dictionary_ListAvailableDictionariesForContact
	@ContactId			INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		[DictionaryView].[Id],
				[DictionaryView].[Name],
				[DictionaryView].[Description]

	FROM		[DictionaryView]

	WHERE		[DictionaryView].[ContactId] IS NULL
		OR		[DictionaryView].[ContactId] = @ContactId
END
GO
	