CREATE VIEW [dbo].[ProjectRevenueDistributionView] AS
	SELECT		[Id],
				[ProjectId],
				[CrmProjectId],
				[Revenue],
				[Audit_CreatedBy],
				[Audit_CreatedOn],
				[Audit_ModifiedBy],
				[Audit_ModifiedOn],
				[Audit_DeletedBy],
				[Audit_DeletedOn],
				[Audit_IsDeleted],
				[Audit_VersionId]	
				
	FROM		[ProjectRevenueDistribution]	WITH (NOLOCK)

	WHERE		[ProjectRevenueDistribution].[Audit_IsDeleted] = 0