ALTER TABLE [dbo].[ProjectCategoryDetailType3]
	ADD CONSTRAINT [FK_ProjectCategoryDetailType3_SurveyPlanning]
	FOREIGN KEY (SurveyPlanningId)
	REFERENCES [SurveyPlanning] (Id)
