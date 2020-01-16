CREATE PROCEDURE Operation_ListAllowedByUser
	@UserId			UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		DISTINCT [OperationView].*
	
	FROM		[OperationView]
	
	LEFT JOIN	[User2OperationView]
		ON		[User2OperationView].[OperationId] = [OperationView].[Id]
		AND		[User2OperationView].[UserId] = @UserId
	
	LEFT JOIN	[UserGroup2OperationView]
		ON		[UserGroup2OperationView].[OperationId] = [OperationView].[Id]
	LEFT JOIN	[User2UserGroupView]
		ON		[User2UserGroupView].[UserGroupId] = [UserGroup2OperationView].[UserGroupId]
		AND		[User2UserGroupView].[UserId] = @UserId
	
	LEFT JOIN	[Role2OperationView]
		ON		[Role2OperationView].[OperationId] = [OperationView].[Id]
	LEFT JOIN	[User2RoleView]
		ON		[User2RoleView].[RoleId] = [Role2OperationView].[RoleId]
		AND		[User2RoleView].[UserId] = @UserId
		
	WHERE		(
					[User2OperationView].[UserId] IS NOT NULL
					OR
					[User2UserGroupView].[UserId] IS NOT NULL
					OR
					[User2RoleView].[UserId] IS NOT NULL
				)

END
GO
