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
	DECLARE @Recipients AS TABLE (RecipientName NVARCHAR(MAX))

	SET @ReportRecipientsString = ''

	INSERT INTO @Recipients
	SELECT		[CrmEmailView].[FirstName] + ' ' + [CrmEmailView].[LastName] 
	FROM		[ProjectCandidateReportRecipientView]

	INNER JOIN	[CrmEmailView]
		ON		[CrmEmailView].[Id] = [ProjectCandidateReportRecipientView].[CrmEmailId]

	WHERE		[ProjectCandidateReportRecipientView].[ProjectCandidateId] = @ProjectCandidateId

	ORDER BY	[CrmEmailView].[LastName]

	INSERT INTO @Recipients

	SELECT		ProjectReportRecipientView.FirstName + ' ' + ProjectReportRecipientView.LastName
	FROM		ProjectReportRecipientView
	WHERE		ProjectReportRecipientView.ProjectId IN (Select ProjectId from ProjectCandidateView where Id = @ProjectCandidateId)

	DECLARE MyCursor CURSOR
	FOR 

	SELECT		R.RecipientName
	FROM		@Recipients R
	
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