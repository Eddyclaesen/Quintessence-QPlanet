CREATE VIEW [dbo].[ProgramComponentSpecialView] AS
	SELECT		[ProgramComponentSpecial].*

	FROM		[ProgramComponentSpecial] WITH (NOLOCK)

	WHERE		[ProgramComponentSpecial].[Audit_IsDeleted] = 0
