CREATE VIEW [dbo].[CulturalFitAssociateView] AS
	SELECT	[CrmReplicationAssociate].[Id]			AS [Id],
			[CrmReplicationAssociate].[UserName]	AS [UserName],
			[CrmReplicationAssociate].[FirstName]	AS [FirstName],
			[CrmReplicationAssociate].[LastName]	AS [LastName],
			[CrmUserEmailView].[Address]			AS [Email],
			[UserView].[Id]							AS [UserId]
	FROM [CrmReplicationAssociate]
	INNER JOIN [UserView] 
		ON [UserView].[AssociateId] =  [CrmReplicationAssociate].[Id]
	INNER JOIN [CrmUserEmailView]
		ON [CrmUserEmailView].[UserId] = [UserView].[Id]