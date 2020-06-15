CREATE PROCEDURE [dbo].[FrameworkAgreement_ListYears]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	YEAR([FrameworkAgreementView].[StartDate])
	FROM	[FrameworkAgreementView]
	WHERE	[FrameworkAgreementView].[StartDate] IS NOT NULL

	UNION

	SELECT	YEAR([FrameworkAgreementView].[EndDate])
	FROM	[FrameworkAgreementView]
	WHERE	[FrameworkAgreementView].[EndDate] IS NOT NULL

END