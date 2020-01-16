CREATE PROCEDURE ProjectCandidate_ListReportRecipients
	@ProjectCandidateId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[ProjectCandidateReportRecipientView].[Id]						AS	[Id],
				NULL															AS	[ProjectId],
				[ProjectCandidateReportRecipientView].[ProjectCandidateId]		AS	[ProjectCandidateId],
				[ProjectCandidateReportRecipientView].[CrmEmailId]				AS	[CrmEmailId],
				[ProjectCandidateReportRecipientView].[FirstName]				AS	[FirstName],
				[ProjectCandidateReportRecipientView].[LastName]				AS	[LastName],
				[ProjectCandidateReportRecipientView].[Email]					AS	[Email],
				[ProjectCandidateReportRecipientView].[DirectPhone]				AS	[DirectPhone],
				[ProjectCandidateReportRecipientView].[MobilePhone]				AS	[MobilePhone]

	FROM		[ProjectCandidateReportRecipientView]

	WHERE       [ProjectCandidateReportRecipientView].[ProjectCandidateId] = @ProjectCandidateId

	UNION

	SELECT		[ProjectReportRecipientView].[Id]								AS	[Id],
				[ProjectReportRecipientView].[ProjectId]						AS	[ProjectId],
				NULL															AS	[ProjectCandidateId],
				[ProjectReportRecipientView].[CrmEmailId]						AS	[CrmEmailId],
				[ProjectReportRecipientView].[FirstName]						AS	[FirstName],
				[ProjectReportRecipientView].[LastName]							AS	[LastName],
				[ProjectReportRecipientView].[Email]							AS	[Email],
				[ProjectReportRecipientView].[DirectPhone]						AS	[DirectPhone],
				[ProjectReportRecipientView].[MobilePhone]						AS	[MobilePhone]

	FROM		[ProjectReportRecipientView]

	INNER JOIN	[ProjectCandidateView]
		ON		[ProjectCandidateView].[Id] = @ProjectCandidateId
		AND		[ProjectCandidateView].[ProjectId] = [ProjectReportRecipientView].[ProjectId]

END
GO