CREATE FUNCTION [dbo].[ProjectCandidate_ReportRecipientsAsString]
(
	@ProjectCandidateId UNIQUEIDENTIFIER,
	@Delimiter			NVARCHAR(1)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE	@ReportRecipientsString NVARCHAR(MAX)
	DECLARE @FullName NVARCHAR(MAX)

	SET @ReportRecipientsString = ''

	DECLARE MyCursor CURSOR
	FOR 

	SELECT		[CrmEmailView].[FirstName] + ' ' + [CrmEmailView].[LastName] 
	FROM		[ProjectCandidateReportRecipientView]

	INNER JOIN	[CrmEmailView]
		ON		[CrmEmailView].[Id] = [ProjectCandidateReportRecipientView].[CrmEmailId]

	WHERE		[ProjectCandidateReportRecipientView].[ProjectCandidateId] = @ProjectCandidateId

	ORDER BY	[CrmEmailView].[LastName]
	
	OPEN MyCursor

	FETCH NEXT
	FROM MyCursor
	INTO @FullName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		SET @ReportRecipientsString = @ReportRecipientsString + @Delimiter + ' ' + @FullName

		FETCH NEXT
			FROM MyCursor
			INTO @FullName
			
	END

	CLOSE MyCursor

	IF @ReportRecipientsString <> ''
	BEGIN
		SET @ReportRecipientsString = SUBSTRING(@ReportRecipientsString, 3, LEN(@ReportRecipientsString) - 2)
	END
	
	RETURN @ReportRecipientsString
END
