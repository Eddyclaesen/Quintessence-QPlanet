CREATE VIEW [dbo].[ConsultancyProjectView] AS
	SELECT		[ConsultancyProject].*

	FROM		[ConsultancyProject]	WITH (NOLOCK)

	INNER JOIN	[ProjectView]
		ON		[ProjectView].[Id] = [ConsultancyProject].[Id]