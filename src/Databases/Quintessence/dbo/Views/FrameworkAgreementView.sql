CREATE VIEW [dbo].[FrameworkAgreementView] AS
	SELECT		[FrameworkAgreement].*,
				[CrmReplicationContact].[Name]					AS ContactName
				
	FROM		[FrameworkAgreement]	WITH (NOLOCK)

	INNER JOIN	[CrmReplicationContact]
		ON		[CrmReplicationContact].[Id] = [FrameworkAgreement].[ContactId]
	
	WHERE		[FrameworkAgreement].[Audit_IsDeleted] = 0