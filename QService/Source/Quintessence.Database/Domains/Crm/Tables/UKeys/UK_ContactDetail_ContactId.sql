ALTER TABLE [dbo].[ContactDetail]
	ADD CONSTRAINT [UK_ContactDetail_ContactId] 
	UNIQUE ([ContactId])