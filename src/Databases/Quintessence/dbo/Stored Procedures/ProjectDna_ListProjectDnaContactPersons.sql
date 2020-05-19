CREATE PROCEDURE [dbo].[ProjectDna_ListProjectDnaContactPersons]
	@ProjectDnaId	UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[CrmReplicationPerson].[Id],
				[CrmReplicationPerson].[FirstName],
				[CrmReplicationPerson].[LastName],
				[CrmReplicationPerson].[Title],
				[CrmReplicationPerson].[IsRetired],
				CASE WHEN [ProjectDna2CrmPersonView].[ProjectDnaId] IS NULL THEN CONVERT(BIT, 0)
					 ELSE CONVERT(BIT, 1)
				END AS [IsSelected]

	FROM		[CrmReplicationPerson]

	INNER JOIN	[ProjectDnaView]
		ON		[ProjectDnaView].[Id] = @ProjectDnaId

	INNER JOIN	[CrmProjectView]
		ON		[CrmProjectView].[Id] = [ProjectDnaView].[CrmProjectId]
		AND		[CrmProjectView].[ContactId] = [CrmReplicationPerson].[ContactId]

	LEFT JOIN	[ProjectDna2CrmPersonView]
		ON		[ProjectDna2CrmPersonView].[CrmPersonId] = [CrmReplicationPerson].[Id]
		AND		[ProjectDna2CrmPersonView].[ProjectDnaId] = @ProjectDnaId
END