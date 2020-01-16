CREATE VIEW [dbo].[CrmUnsynchronizedEmployeeView] AS
	SELECT		[CrmEmployeeView].* 

	FROM		[CrmEmployeeView]

	LEFT JOIN	[UserView]
		ON		[UserView].[AssociateId] = [CrmEmployeeView].[Id]

	WHERE		[UserView].[Id] IS NULL