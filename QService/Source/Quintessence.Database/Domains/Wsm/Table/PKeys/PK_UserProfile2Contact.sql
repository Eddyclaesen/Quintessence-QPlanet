ALTER TABLE [dbo].[UserProfile2Contact]
	ADD CONSTRAINT [PK_UserProfile2Contact] 
	PRIMARY KEY NONCLUSTERED ([UserProfileId], [ContactId])