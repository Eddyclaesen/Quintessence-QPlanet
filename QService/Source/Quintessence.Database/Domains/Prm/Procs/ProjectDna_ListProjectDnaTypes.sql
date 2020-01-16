CREATE PROCEDURE [dbo].[ProjectDna_ListProjectDnaTypes]
	@ProjectDnaId	UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		[ProjectDnaTypeView].[Id],
				[ProjectDnaTypeView].[Name],
				CASE WHEN [ProjectDna2ProjectDnaTypeView].[ProjectDnaId] IS NULL THEN CONVERT(BIT, 0)
					 ELSE CONVERT(BIT, 1)
				END AS [IsSelected]

	FROM		[ProjectDnaTypeView]

	LEFT JOIN	[ProjectDna2ProjectDnaTypeView]
		ON		[ProjectDna2ProjectDnaTypeView].[ProjectDnaTypeId] = [ProjectDnaTypeView].[Id]
		AND		[ProjectDna2ProjectDnaTypeView].[ProjectDnaId] = @ProjectDnaId
END
GO
