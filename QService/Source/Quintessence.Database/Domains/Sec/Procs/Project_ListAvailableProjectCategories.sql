CREATE PROCEDURE Project_ListAvailableProjectCategories
	@ProjectTypeId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		*
	
	FROM		[ProjectCategoryView]
	
	INNER JOIN	[ProjectType2ProjectCategory]
		ON		[ProjectType2ProjectCategory].[ProjectCategoryId] = [ProjectCategoryView].[Id]

	WHERE		[ProjectType2ProjectCategory].[ProjectTypeId] = @ProjectTypeId
END
GO
