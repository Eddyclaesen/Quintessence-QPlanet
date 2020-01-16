CREATE VIEW [dbo].[CrmEmployeeView] AS
	SELECT		[CrmReplicationAssociate].[Id]					AS	[Id],
				[CrmReplicationAssociate].[UserName]			AS	[UserName],
				[CrmReplicationAssociate].[FirstName]			AS	[FirstName],
				[CrmReplicationAssociate].[LastName]			AS	[LastName]

	FROM		[CrmReplicationAssociate]

	WHERE		[CrmReplicationAssociate].[UserGroupId] <> 20