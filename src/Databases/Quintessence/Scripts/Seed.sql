DECLARE @createdBy VARCHAR(250)
DECLARE @createdOn DATETIME2
DECLARE @userId UNIQUEIDENTIFIER
DECLARE @password VARCHAR(250)

SET @createdBy = 'Database Administrator'
SET @createdOn = GETDATE()
SET @password = 'FXpCrnkIJ1sjnlSJpEpSQQ==' -- kenze.123!


/* Kenze Accountant */

SET @userId = '70FD2BAB-7D85-483B-B627-57B5CAD0F694'

IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE [Id] = @userId)
BEGIN

INSERT INTO dbo.[User] 
	(
	[Id], 
	[AssociateId], 
	[RoleId], 
	[UserName], 
	[FirstName], 
	[LastName], 
	[Password], 
	[ChangePassword], 
	[IsEmployee], 
	[Audit_CreatedBy], 
	[Audit_CreatedOn]
	)
VALUES
	(
	@userId,
	-1,
	'F52EC0A3-F44A-4CD7-B8AA-C46A019088DD',
	'Kenze_Accountant',
	'Kenze',
	'Accountant',
	'FXpCrnkIJ1sjnlSJpEpSQQ==',
	0,
	1,
	@createdBy,
	@createdOn
	)

INSERT INTO dbo.[UserProfile]
	(
	[Id],
	[UserId],
	[LanguageId],
	[Audit_CreatedBy],
	[Audit_CreatedOn]
	)
VALUES
	(
	@userId,
	@userId,
	1,
	@createdBy,
	@createdOn
	)
END


/* Kenze CustomerAssitant */

SET @userId = 'B64A4298-0C02-4327-B53F-0205CA74BD4C'

IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE [Id] = @userId)
BEGIN

INSERT INTO dbo.[User] 
	(
	[Id], 
	[AssociateId], 
	[RoleId], 
	[UserName], 
	[FirstName], 
	[LastName], 
	[Password], 
	[ChangePassword], 
	[IsEmployee], 
	[Audit_CreatedBy], 
	[Audit_CreatedOn]
	)
VALUES
	(
	@userId,
	-1,
	'D3D0E788-BED4-49EF-A93E-78542D7296CE',
	'Kenze_CustomerAssistant',
	'Kenze',
	'CustomerAssistant',
	'FXpCrnkIJ1sjnlSJpEpSQQ==',
	0,
	1,
	@createdBy,
	@createdOn
	)

INSERT INTO dbo.[UserProfile]
	(
	[Id],
	[UserId],
	[LanguageId],
	[Audit_CreatedBy],
	[Audit_CreatedOn]
	)
VALUES
	(
	@userId,
	@userId,
	1,
	@createdBy,
	@createdOn
	)
END

/* Kenze Admin */

SET @userId = '00FF468F-2B79-4ABC-B51E-6B470A6F8442'

IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE [Id] = @userId)
BEGIN

INSERT INTO dbo.[User] 
	(
	[Id], 
	[AssociateId], 
	[RoleId], 
	[UserName], 
	[FirstName], 
	[LastName], 
	[Password], 
	[ChangePassword], 
	[IsEmployee], 
	[Audit_CreatedBy], 
	[Audit_CreatedOn]
	)
VALUES
	(
	@userId,
	-1,
	'E60C52B1-942B-4735-BFD9-5C05E542C79E',
	'Kenze_Admin',
	'Kenze',
	'Admin',
	'FXpCrnkIJ1sjnlSJpEpSQQ==',
	0,
	1,
	@createdBy,
	@createdOn
	)

INSERT INTO dbo.[UserProfile]
	(
	[Id],
	[UserId],
	[LanguageId],
	[Audit_CreatedBy],
	[Audit_CreatedOn]
	)
VALUES
	(
	@userId,
	@userId,
	1,
	@createdBy,
	@createdOn
	)
END

