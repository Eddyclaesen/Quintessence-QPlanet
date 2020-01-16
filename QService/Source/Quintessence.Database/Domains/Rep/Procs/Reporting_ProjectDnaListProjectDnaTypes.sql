CREATE PROCEDURE [dbo].[Reporting_ProjectDnaListProjectDnaTypes]
AS
BEGIN
	SELECT		[Id], [Name]
	FROM		[ProjectDnaTypeView]
END
GO