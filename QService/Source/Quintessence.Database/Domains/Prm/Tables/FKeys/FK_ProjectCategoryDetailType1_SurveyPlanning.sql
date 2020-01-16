ALTER TABLE [dbo].[ProjectCategoryDetailType1]
	ADD CONSTRAINT [FK_ProjectCategoryDetailType1_SurveyPlanning]
	FOREIGN KEY (SurveyPlanningId)
	REFERENCES [SurveyPlanning] (Id)
