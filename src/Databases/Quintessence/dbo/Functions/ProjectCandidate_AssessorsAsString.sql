CREATE FUNCTION [dbo].[ProjectCandidate_AssessorsAsString]
(
	@Code			NVARCHAR(12),
	@Delimiter		NVARCHAR(1)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE	@AssessorsString NVARCHAR(MAX)
	DECLARE @FullName NVARCHAR(MAX)
	DECLARE @Order INT

	SET @AssessorsString = ''

	DECLARE MyCursor CURSOR
	FOR 

	SELECT		[UserView].[FirstName] + ' ' + [UserView].[LastName],
				CASE WHEN [CrmReplicationAppointment].[TaskId] = 190 THEN 2 ELSE 1 END		AS	[Order]

	FROM		[CrmReplicationAppointment]

	INNER JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmReplicationAppointment].[AssociateId]

	WHERE		[CrmReplicationAppointment].[Code] = @Code

	ORDER BY	[Order],
				[UserView].[LastName]
	
	OPEN MyCursor

	FETCH NEXT
	FROM MyCursor
	INTO @FullName, @Order

	WHILE @@FETCH_STATUS = 0
	BEGIN

		SET @AssessorsString = @AssessorsString + @Delimiter + ' ' + @FullName

		FETCH NEXT
			FROM MyCursor
			INTO @FullName, @Order
			
	END

	CLOSE MyCursor

	IF @AssessorsString <> ''
	BEGIN
		SET @AssessorsString = SUBSTRING(@AssessorsString, 3, LEN(@AssessorsString) - 2)
	END
	
	RETURN @AssessorsString
END