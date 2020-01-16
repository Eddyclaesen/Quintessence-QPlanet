ALTER TABLE [dbo].[UserProfile2Contact]
	ADD CONSTRAINT [FK_UserProfile2Contact_Contact] 
	FOREIGN KEY([UserProfileId])
	REFERENCES [dbo].[UserProfile] ([Id])