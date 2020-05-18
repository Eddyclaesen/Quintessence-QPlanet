CREATE VIEW [dbo].[EmployeeView] AS 
	SELECT		[Employee].*

	FROM		[Employee]	WITH (NOLOCK)

	INNER JOIN	[UserView]
		ON		[UserView].[Id] = [Employee].[Id]
		AND		[UserView].[IsEmployee] = 1