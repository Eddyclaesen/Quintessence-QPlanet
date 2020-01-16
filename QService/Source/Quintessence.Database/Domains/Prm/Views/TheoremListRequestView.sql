CREATE VIEW [dbo].[TheoremListRequestView] AS
	SELECT		[TheoremListRequest].*,
				[CrmEmailView].[FirstName]														AS	[FirstName],
				[CrmEmailView].[LastName]														AS	[LastName],
				[CrmEmailView].[Email]															AS	[Email],
				[TheoremListRequestType].[Type]													AS  [TheoremListRequestType],
				[ProjectView].[Name]															AS  [ProjectName],
				[dbo].[TheoremListRequest_CheckCulturalFitStatus]([TheoremListRequest].[Id])	AS  [Status]

	FROM		[TheoremListRequest]	WITH (NOLOCK)

	LEFT JOIN	[CrmEmailView]
		ON		[CrmEmailView].[Id] = [TheoremListRequest].[CrmEmailId]

	INNER JOIN	[TheoremListRequestType]
		ON		[TheoremListRequestType].[Id] = [TheoremListRequest].[TheoremListRequestTypeId]

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [TheoremListRequest].[ProjectId]

