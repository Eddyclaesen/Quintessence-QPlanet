CREATE PROCEDURE [dbo].[Reporting_RetrieveTranslation] 
	@Code				NVARCHAR(MAX),
	@LanguageId			INT
AS
BEGIN
	SELECT      *

	FROM        [ReportParameterValueView]

	WHERE       [ReportParameterValueView].[ReportParameterCode] = @Code
		AND		[ReportParameterValueView].[LanguageId] = @LanguageId
END