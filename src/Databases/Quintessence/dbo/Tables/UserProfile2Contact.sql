CREATE TABLE [dbo].[UserProfile2Contact] (
    [UserProfileId] UNIQUEIDENTIFIER NOT NULL,
    [ContactId]     INT              NOT NULL,
    CONSTRAINT [PK_UserProfile2Contact] PRIMARY KEY NONCLUSTERED ([UserProfileId] ASC, [ContactId] ASC),
    CONSTRAINT [FK_UserProfile2Contact_Contact] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[UserProfile] ([Id])
);

