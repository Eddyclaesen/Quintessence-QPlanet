BEGIN TRAN
--Neopir
INSERT INTO dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT      NEWID(),
				dbo.Project.id,
				'AB11C1FF-1360-4D9F-8F4E-22B46C4D8AB5',
				0
	             
	FROM         actserver.Act.[dbo].[SelectedTests] [SelectedTests]

	INNER JOIN   actserver.Act.[dbo].[Testen]  [Testen]
		   ON     [Testen].[Test_ID] = [SelectedTests].[Test_ID]

	INNER JOIN   actserver.Act.[dbo].[ProjFiche_AcSim_Rel] [ProjFiche_AcSim_Rel]
		   ON     [ProjFiche_AcSim_Rel].[ACSimulatieSet_ID] = [SelectedTests].[ACSimulatieSetID]

	INNER JOIN   actserver.Act.[dbo].[ProjectFiche] [ProjectFiche]
		   ON     [ProjectFiche].[ACProject_ID] = [ProjFiche_AcSim_Rel].[ACProject_ID]

	INNER JOIN	dbo.Project
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]   

	LEFT JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
		AND		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = 'AB11C1FF-1360-4D9F-8F4E-22B46C4D8AB5'

	LEFT JOIN	dbo.ProjectCategoryDetailType3
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailType3.Id

	WHERE		[Testen].Omschrijving = 'NeoPir'
		AND		dbo.ProjectCategoryDetailType3.Id IS NULL
		
--Leiderschapsstijlentest
INSERT INTO dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT      NEWID(),
				dbo.Project.id,
				'1C4A53D1-31EA-4774-BB27-9A6D73DDEF99',
				0
	             
	FROM         actserver.Act.[dbo].[SelectedTests] [SelectedTests]

	INNER JOIN   actserver.Act.[dbo].[Testen]  [Testen]
		   ON     [Testen].[Test_ID] = [SelectedTests].[Test_ID]

	INNER JOIN   actserver.Act.[dbo].[ProjFiche_AcSim_Rel] [ProjFiche_AcSim_Rel]
		   ON     [ProjFiche_AcSim_Rel].[ACSimulatieSet_ID] = [SelectedTests].[ACSimulatieSetID]

	INNER JOIN   actserver.Act.[dbo].[ProjectFiche] [ProjectFiche]
		   ON     [ProjectFiche].[ACProject_ID] = [ProjFiche_AcSim_Rel].[ACProject_ID]

	INNER JOIN	dbo.Project
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]   

	LEFT JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
		AND		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = '1C4A53D1-31EA-4774-BB27-9A6D73DDEF99'

	LEFT JOIN	dbo.ProjectCategoryDetailType3
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailType3.Id

	WHERE		[Testen].Omschrijving = 'Leiderschapsstijlentest'
		AND		dbo.ProjectCategoryDetailType3.Id IS NULL

--NeoFFI
INSERT INTO dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT      NEWID(),
				dbo.Project.id,
				'AB11C1FF-1360-4D9F-8F4E-22B46C4D8AB5',
				0
	             
	FROM         actserver.Act.[dbo].[SelectedTests] [SelectedTests]

	INNER JOIN   actserver.Act.[dbo].[Testen]  [Testen]
		   ON     [Testen].[Test_ID] = [SelectedTests].[Test_ID]

	INNER JOIN   actserver.Act.[dbo].[ProjFiche_AcSim_Rel] [ProjFiche_AcSim_Rel]
		   ON     [ProjFiche_AcSim_Rel].[ACSimulatieSet_ID] = [SelectedTests].[ACSimulatieSetID]

	INNER JOIN   actserver.Act.[dbo].[ProjectFiche] [ProjectFiche]
		   ON     [ProjectFiche].[ACProject_ID] = [ProjFiche_AcSim_Rel].[ACProject_ID]

	INNER JOIN	dbo.Project
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]   

	LEFT JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
		AND		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = 'AB11C1FF-1360-4D9F-8F4E-22B46C4D8AB5'

	LEFT JOIN	dbo.ProjectCategoryDetailType3
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailType3.Id

	WHERE		[Testen].Omschrijving = 'NeoFFI'
		AND		dbo.ProjectCategoryDetailType3.Id IS NULL
		
--Leerstijlentest	
INSERT INTO dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT      NEWID(),
				dbo.Project.id,
				'E0FB4B74-983D-4912-A58B-31A7EBAFCED1',
				0
	             
	FROM         actserver.Act.[dbo].[SelectedTests] [SelectedTests]

	INNER JOIN   actserver.Act.[dbo].[Testen]  [Testen]
		   ON     [Testen].[Test_ID] = [SelectedTests].[Test_ID]

	INNER JOIN   actserver.Act.[dbo].[ProjFiche_AcSim_Rel] [ProjFiche_AcSim_Rel]
		   ON     [ProjFiche_AcSim_Rel].[ACSimulatieSet_ID] = [SelectedTests].[ACSimulatieSetID]

	INNER JOIN   actserver.Act.[dbo].[ProjectFiche] [ProjectFiche]
		   ON     [ProjectFiche].[ACProject_ID] = [ProjFiche_AcSim_Rel].[ACProject_ID]

	INNER JOIN	dbo.Project
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]   

	LEFT JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
		AND		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = 'E0FB4B74-983D-4912-A58B-31A7EBAFCED1'

	LEFT JOIN	dbo.ProjectCategoryDetailtype2
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailtype2.Id

	WHERE		[Testen].Omschrijving = 'Leerstijlentest'
		AND		dbo.ProjectCategoryDetailtype2.Id IS NULL
		
--Belbin	
INSERT INTO dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT      NEWID(),
				dbo.Project.id,
				'9F29ADAB-425C-4D48-82B6-18E7CBE0DDF2',
				0
	             
	FROM         actserver.Act.[dbo].[SelectedTests] [SelectedTests]

	INNER JOIN   actserver.Act.[dbo].[Testen]  [Testen]
		   ON     [Testen].[Test_ID] = [SelectedTests].[Test_ID]

	INNER JOIN   actserver.Act.[dbo].[ProjFiche_AcSim_Rel] [ProjFiche_AcSim_Rel]
		   ON     [ProjFiche_AcSim_Rel].[ACSimulatieSet_ID] = [SelectedTests].[ACSimulatieSetID]

	INNER JOIN   actserver.Act.[dbo].[ProjectFiche] [ProjectFiche]
		   ON     [ProjectFiche].[ACProject_ID] = [ProjFiche_AcSim_Rel].[ACProject_ID]

	INNER JOIN	dbo.Project
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]   

	LEFT JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
		AND		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = '9F29ADAB-425C-4D48-82B6-18E7CBE0DDF2'

	LEFT JOIN	dbo.ProjectCategoryDetailtype2
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailtype2.Id

	WHERE		[Testen].Omschrijving = 'Belbin'
		AND		dbo.ProjectCategoryDetailtype2.Id IS NULL
		
--Watson Glaser Test	
INSERT INTO dbo.ProjectCategoryDetail(Id, ProjectId, ProjectTypeCategoryId, UnitPrice)
	SELECT      NEWID(),
				dbo.Project.id,
				'FEA5B9C6-F091-4844-BE08-2F15C6CD0981',
				0
	             
	FROM         actserver.Act.[dbo].[SelectedTests] [SelectedTests]

	INNER JOIN   actserver.Act.[dbo].[Testen]  [Testen]
		   ON     [Testen].[Test_ID] = [SelectedTests].[Test_ID]

	INNER JOIN   actserver.Act.[dbo].[ProjFiche_AcSim_Rel] [ProjFiche_AcSim_Rel]
		   ON     [ProjFiche_AcSim_Rel].[ACSimulatieSet_ID] = [SelectedTests].[ACSimulatieSetID]

	INNER JOIN   actserver.Act.[dbo].[ProjectFiche] [ProjectFiche]
		   ON     [ProjectFiche].[ACProject_ID] = [ProjFiche_AcSim_Rel].[ACProject_ID]

	INNER JOIN	dbo.Project
		ON		dbo.Project.LegacyId = [ProjectFiche].[Project_ID]   

	LEFT JOIN	dbo.ProjectCategoryDetail
		ON		dbo.ProjectCategoryDetail.ProjectId = dbo.Project.Id
		AND		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = 'FEA5B9C6-F091-4844-BE08-2F15C6CD0981'

	LEFT JOIN	dbo.ProjectCategoryDetailtype2
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailtype2.Id

	WHERE		[Testen].Omschrijving = 'Watson Glaser Test'
		AND		dbo.ProjectCategoryDetailtype2.Id IS NULL
		
		

INSERT INTO	dbo.ProjectCategoryDetailType3
	SELECT		dbo.ProjectCategoryDetail.Id,
				dbo.ProjectTypeCategory.Name,
				'',
				1,
				1,
				'',
				''

	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = dbo.ProjectTypeCategory.Id
		AND		dbo.ProjectTypeCategory.SubCategoryType = 3

	LEFT JOIN	dbo.ProjectCategoryDetailType3
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailType3.Id

	WHERE		dbo.ProjectCategoryDetailType3.Id IS NULL


INSERT INTO	dbo.ProjectCategoryDetailType2
	SELECT		dbo.ProjectCategoryDetail.Id,
				dbo.ProjectTypeCategory.Name,
				'',
				1

	FROM		dbo.ProjectCategoryDetail

	INNER JOIN	dbo.ProjectTypeCategory
		ON		dbo.ProjectCategoryDetail.ProjectTypeCategoryId = dbo.ProjectTypeCategory.Id
		AND		dbo.ProjectTypeCategory.SubCategoryType = 2

	LEFT JOIN	dbo.ProjectCategoryDetailType2
		ON		dbo.ProjectCategoryDetail.Id = dbo.ProjectCategoryDetailType2.Id

	WHERE		dbo.ProjectCategoryDetailType2.Id IS NULL
COMMIT