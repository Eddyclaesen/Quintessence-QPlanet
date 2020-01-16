ALTER TABLE [dbo].[AssessmentRoom]  
	ADD CONSTRAINT [FK_AssessmentRoom_Office] 
	FOREIGN KEY([OfficeId])
	REFERENCES [dbo].[Office] ([Id])