CREATE VIEW [dbo].[ReportParameterValueView] AS
	SELECT		[ReportParameterValue].*			,
				[ReportParameterView].[Code]		AS	[ReportParameterCode]

	FROM		[ReportParameterValue]	WITH (NOLOCK)

	INNER JOIN	[ReportParameterView]
		ON		[ReportParameterView].[Id] = [ReportParameterValue].[ReportParameterId]

	WHERE		[ReportParameterValue].[Audit_IsDeleted] = 0