CREATE VIEW [dbo].[AssessmentRoomView]
	AS SELECT	[AssessmentRoom].*,
				[OfficeView].[ShortName]		AS	[OfficeShortName],
				[OfficeView].[FullName]			AS	[OfficeFullName]

	FROM		[AssessmentRoom]	WITH (NOLOCK)

	INNER JOIN	[OfficeView]
		ON		[OfficeView].[Id] = [AssessmentRoom].[OfficeId]

	WHERE		[AssessmentRoom].[Audit_IsDeleted] = 0
