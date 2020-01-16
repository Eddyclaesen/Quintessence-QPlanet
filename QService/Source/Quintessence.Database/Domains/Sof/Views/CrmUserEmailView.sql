CREATE VIEW [dbo].[CrmUserEmailView] AS
	SELECT		[CrmReplicationEmailAssociate].[Id]				AS	[Id],
				[UserView].[Id]									AS	[UserId],
				[UserView].[Id]									AS	[UserProfileId],
				[CrmReplicationEmailAssociate].[Email]			AS	[Address],
				[CrmReplicationEmailAssociate].[Rank]			AS	[Rank]

	FROM		[CrmReplicationEmailAssociate]	WITH (NOLOCK)

	INNER JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmReplicationEmailAssociate].[AssociateId]