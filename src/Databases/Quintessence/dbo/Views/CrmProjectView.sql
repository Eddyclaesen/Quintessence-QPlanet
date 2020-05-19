CREATE VIEW [dbo].[CrmProjectView] AS
	SELECT		[Id],
				[Name],
				[AssociateId],
				[ContactId],
				[ProjectStatusId],
				[StartDate],
				[BookyearFrom],
				[BookyearTo]

	FROM		[CrmProject]