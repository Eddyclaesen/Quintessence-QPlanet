ALTER TABLE [dbo].[UserProfile]  
	ADD CONSTRAINT [FK_UserProfile_User] 
	FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])