CREATE VIEW [dbo].[ProjectRoleDictionaryLevelView]
	AS 
	SELECT		pr2dl.ProjectRoleId			AS	ProjectRoleId, 
				pr.Name						AS	ProjectRoleName, 
				pr.ContactId				AS	ContactId, 
				di.Id						As	DictionaryIndicatorId,
				di.Name						AS	DictionaryIndicatorName,
				di.[Order]					AS	DictionaryIndicatorOrder,
				pr2dl.DictionaryLevelId		AS	DictionaryLevelId, 
				dl.Name						AS	DictionaryLevelName,
				dl.[Level]					AS	DictionaryLevelLevel,
				dco.Id						AS	DictionaryCompetenceId, 
				dco.Name					AS	DictionaryCompetenceName, 
				dco.[Order]					AS	DictionaryCompetenceOrder,
				dcl.Id						AS	DictionaryClusterId, 
				dcl.Name					AS	DictionaryClusterName, 
				dcl.[Order]					AS	DictionaryClusterOrder,
				dic.Id						AS	DictionaryId, 
				dic.Name					AS	DictionaryName,
				CASE pr2di.Norm
					WHEN 10 THEN CAST(1 AS BIT)
					ELSE CAST(0 AS BIT)
				END	AS IsStandard,
				CASE pr2di.Norm
					WHEN 20 THEN CAST(1 AS BIT)
					ELSE CAST(0 AS BIT)
				END	AS IsDistinctive
				
	FROM		dbo.ProjectRole2DictionaryLevel AS pr2dl	WITH (NOLOCK) 

	INNER JOIN	dbo.ProjectRoleView AS pr
		ON		pr2dl.ProjectRoleId = pr.Id 

	INNER JOIN	dbo.DictionaryLevelView AS dl
		ON		pr2dl.DictionaryLevelId = dl.Id

	INNER JOIN	dbo.DictionaryIndicatorView AS di
		ON		di.DictionaryLevelId = dl.Id   

	INNER JOIN	dbo.DictionaryCompetenceView AS dco
		ON		dl.DictionaryCompetenceId = dco.Id 

	INNER JOIN	dbo.DictionaryClusterView AS dcl
		ON		dco.DictionaryClusterId = dcl.Id 

	INNER JOIN	dbo.DictionaryView AS dic
		ON		dcl.DictionaryId = dic.Id

	LEFT JOIN	dbo.ProjectRole2DictionaryIndicator AS pr2di
		ON		pr2di.DictionaryIndicatorId = di.Id
		AND		pr2di.ProjectRoleId = pr2dl.ProjectRoleId