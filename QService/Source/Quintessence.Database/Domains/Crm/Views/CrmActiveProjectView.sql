CREATE VIEW [dbo].[CrmActiveProjectView] AS
	SELECT		*
	FROM		[CrmProjectView]
	WHERE		[CrmProjectView].ProjectStatusId IN (2,3)