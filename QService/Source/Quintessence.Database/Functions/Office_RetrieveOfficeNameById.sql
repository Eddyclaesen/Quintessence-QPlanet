CREATE FUNCTION [dbo].[Office_RetrieveOfficeNameById]
(
	@OfficeId INT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @OfficeName NVARCHAR(MAX)

	SELECT @OfficeName = [OfficeView].[ShortName] 
	FROM [OfficeView] 
	WHERE [OfficeView].[Id] = @OfficeId

	RETURN @OfficeName
END
