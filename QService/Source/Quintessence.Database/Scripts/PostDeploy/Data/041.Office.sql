DECLARE @OfficeId INT

IF NOT EXISTS(SELECT * FROM Office WHERE ShortName = 'QA')
BEGIN
	INSERT INTO Office(ShortName, FullName) VALUES ('QA', 'Quintessence Antwerpen')
	SELECT @OfficeId = Id FROM Office WHERE ShortName = 'QA'

	/*TRANSLATION*/
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 1, 'Quintessence Antwerpen')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 2, 'Quintessence Anvers')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 3, 'Quintessence Antwerp')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 4, 'Quintessence Antwerpen')
END

IF NOT EXISTS(SELECT * FROM Office WHERE ShortName = 'QB')
BEGIN
	INSERT INTO Office(ShortName, FullName) VALUES ('QB', 'Quintessence Brussel')
	SELECT @OfficeId = Id FROM Office WHERE ShortName = 'QB'

	/*TRANSLATION*/
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 1, 'Quintessence Brussel')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 2, 'Quintessence Bruxelles')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 3, 'Quintessence Brussels')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 4, 'Quintessence Brüssel')
END

IF NOT EXISTS(SELECT * FROM Office WHERE ShortName = 'QG')
BEGIN
	INSERT INTO Office(ShortName, FullName) VALUES ('QG', 'Quintessence Gent')
	SELECT @OfficeId = Id FROM Office WHERE ShortName = 'QG'

	/*TRANSLATION*/
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 1, 'Quintessence Gent')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 2, 'Quintessence Gand')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 3, 'Quintessence Ghent')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 4, 'Quintessence Gent')
END

IF NOT EXISTS(SELECT * FROM Office WHERE ShortName = 'CU')
BEGIN
	INSERT INTO Office(ShortName, FullName) VALUES ('Cu', 'At customer')
	SELECT @OfficeId = Id FROM Office WHERE ShortName = 'CU'

	/*TRANSLATION*/
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 1, 'Bij de klant')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 2, 'Chez le client')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 3, 'At the customer')
	INSERT INTO OfficeTranslation(OfficeId, LanguageId, Name) VALUES (@OfficeId, 4, 'Beim Kunden')
END

DECLARE	@QaId AS INT
SELECT	@QaId = [Id]	FROM	[OfficeView]	WHERE	[ShortName] = 'QA'

DECLARE	@QbId AS INT
SELECT	@QbId = [Id]	FROM	[OfficeView]	WHERE	[ShortName] = 'QB'

DECLARE	@QgId AS INT
SELECT	@QgId = [Id]	FROM	[OfficeView]	WHERE	[ShortName] = 'QG'

INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 1', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 2', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 3', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 4', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 5', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 6', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 7', @QaId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 8', @QaId)

INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 1', @QbId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 2', @QbId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 3', @QbId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 4', @QbId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 5', @QbId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 6', @QbId)

INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 1', @QgId)
INSERT INTO [AssessmentRoom]([Id], [Name], [OfficeId]) VALUES(NEWID(), 'Room 2', @QgId)

INSERT INTO [AssessorColor]([Color]) VALUES ('#FFC080')
INSERT INTO [AssessorColor]([Color]) VALUES ('#FF8000')
INSERT INTO [AssessorColor]([Color]) VALUES ('#FFFF80')
INSERT INTO [AssessorColor]([Color]) VALUES ('#C0C000')
INSERT INTO [AssessorColor]([Color]) VALUES ('#00FF00')
INSERT INTO [AssessorColor]([Color]) VALUES ('#00C000')
INSERT INTO [AssessorColor]([Color]) VALUES ('#80FFFF')
INSERT INTO [AssessorColor]([Color]) VALUES ('#00C0C0')
INSERT INTO [AssessorColor]([Color]) VALUES ('#8080FF')
INSERT INTO [AssessorColor]([Color]) VALUES ('#0000FF')
INSERT INTO [AssessorColor]([Color]) VALUES ('#FF80FF')
INSERT INTO [AssessorColor]([Color]) VALUES ('#FF00FF')
INSERT INTO [AssessorColor]([Color]) VALUES ('#FF8080')
INSERT INTO [AssessorColor]([Color]) VALUES ('#FF0000')
INSERT INTO [AssessorColor]([Color]) VALUES ('#E0E0E0')
INSERT INTO [AssessorColor]([Color]) VALUES ('#808080')