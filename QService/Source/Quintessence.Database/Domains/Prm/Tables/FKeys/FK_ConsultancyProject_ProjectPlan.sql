ALTER TABLE [dbo].[ConsultancyProject]  
	ADD CONSTRAINT [FK_ConsultancyProject_ProjectPlan] 
	FOREIGN KEY([ProjectPlanId])
	REFERENCES [dbo].[ProjectPlan] ([Id])