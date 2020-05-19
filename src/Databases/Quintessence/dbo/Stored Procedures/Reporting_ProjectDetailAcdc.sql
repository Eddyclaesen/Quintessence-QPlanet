CREATE PROCEDURE Reporting_ProjectDetailAcdc
	@ProjectId	UNIQUEIDENTIFIER
AS
BEGIN
	SELECT		ProjectView.Name										AS ProjectName,
				ProjectTypeCategoryView.Name							AS ProjectMainCategory,
				DictionaryView.Name										AS DictionaryName,
				AssessmentDevelopmentProjectView.FunctionTitle			AS FunctionTitle,
				ContactDetailView.Remarks								AS CustomerInformation,
				ProjectView.DepartmentInformation						AS DepartmentInformation,
				AssessmentDevelopmentProjectView.FunctionInformation	AS FunctionInformation,
				UserView.FirstName + ' ' + UserView.LastName			AS ProjectManager,
				CrmContactView.name										AS Customer

	FROM		ProjectView

	INNER JOIN	ProjectTypeView
		ON		ProjectTypeView.Id = ProjectView.ProjectTypeId

	INNER JOIN	AssessmentDevelopmentProjectView
		ON		AssessmentDevelopmentProjectView.Id = ProjectView.Id
		
	INNER JOIN	ProjectCategoryDetailView MainProjectCategoryDetail
		ON		MainProjectCategoryDetail.ProjectId = ProjectView.Id

	INNER JOIN	ProjectType2ProjectTypeCategory
		ON		ProjectType2ProjectTypeCategory.ProjectTypeId = ProjectView.ProjectTypeId
		AND		ProjectType2ProjectTypeCategory.IsMain = 1
		AND		ProjectType2ProjectTypeCategory.ProjectTypeCategoryId = MainProjectCategoryDetail.ProjectTypeCategoryId

	INNER JOIN	ProjectTypeCategoryView
		ON		ProjectTypeCategoryView.Id = MainProjectCategoryDetail.ProjectTypeCategoryId
		
	LEFT JOIN	DictionaryView
		ON		DictionaryView.Id = AssessmentDevelopmentProjectView.DictionaryId

	INNER JOIN	ContactDetailView
		ON		ContactDetailView.ContactId = ProjectView.ContactId
		
	INNER JOIN	UserView
		ON		UserView.Id = ProjectView.ProjectManagerId

	INNER JOIN	CrmContactView
		ON		CrmContactView.Id = ProjectView.ContactId	

	WHERE		ProjectView.Id = @ProjectId
END