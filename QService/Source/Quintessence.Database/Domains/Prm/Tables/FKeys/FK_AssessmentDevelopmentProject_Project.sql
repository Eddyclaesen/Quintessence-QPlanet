ALTER TABLE [dbo].[AssessmentDevelopmentProject]  
	ADD CONSTRAINT [FK_AssessmentDevelopmentProject_Project] 
	FOREIGN KEY([Id])
	REFERENCES [dbo].[Project] ([Id])