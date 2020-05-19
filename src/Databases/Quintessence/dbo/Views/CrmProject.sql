CREATE VIEW [dbo].[CrmProject]
AS
	SELECT		[Id],
				[Name],
				[AssociateId],
				[ContactId],
				[ProjectStatusId],
				[StartDate],
				[BookyearFrom],
				[BookyearTo]

	FROM		[CrmReplicationProject]