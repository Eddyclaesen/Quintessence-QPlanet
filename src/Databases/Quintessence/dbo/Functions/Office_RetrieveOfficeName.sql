CREATE FUNCTION [dbo].[Office_RetrieveOfficeName]
(
	@Text NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @OfficeId INT
	DECLARE @OfficeName NVARCHAR(MAX)

	SET @OfficeId = dbo.Office_RetrieveOfficeId(SUBSTRING(@Text, 1, 3))

	SELECT @OfficeName = [OfficeView].[ShortName] 
	FROM [OfficeView] 
	WHERE [OfficeView].[Id] = @OfficeId

	RETURN @OfficeName
END