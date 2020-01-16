ALTER TABLE [dbo].[ProjectCategoryDetailType2]
	ADD CONSTRAINT [FK_ProjectCategoryDetailType2_SurveyPlanning]
	FOREIGN KEY (SurveyPlanningId)
	REFERENCES [SurveyPlanning] (Id)
