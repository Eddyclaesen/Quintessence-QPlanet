DECLARE @DefaultTheoremListTemplateId AS UNIQUEIDENTIFIER
DECLARE @TempTheoremTemplateId AS UNIQUEIDENTIFIER
DECLARE @SystemUser AS NVARCHAR(50)

SELECT @DefaultTheoremListTemplateId = NEWID()
SELECT @SystemUser = SYSTEM_USER

IF NOT EXISTS(SELECT * FROM [TheoremListTemplate] WHERE Name LIKE 'Template for employees')
BEGIN
INSERT INTO TheoremListTemplate (Id, Name, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@DefaultTheoremListTemplateId, 'Template for employees', @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat er een hiërarchische structuur heerst in de organisatie.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important d''avoir une structure hiérarchique qui prévaut dans l''organisation.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to have a hierarchical structure that prevails in an organization.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat binnen een organisatie elke werknemer geniet van een zekere vrijheid en onafhankelijkheid.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important que chaque employé au sein d''une organisation jouisse d''une certaine liberté et indépendance.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that every employee within an organization enjoys a certain freedom and independence.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk om te werken in een organisatie waar de stelling "Eén voor allen, allen voor één" geldt.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler dans une organisation où l’expression «Un pour tous et tous pour un» s’applique.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization where the expression "One for all and all for one" is a reality.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind de volgende waarden binnen een organisatie belangrijk: competitiviteit en prestatiedrang.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve que les valeurs suivantes  sont importantes pour une organisation: la compétitivité et l''esprit de compétition.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find the following values are important to an organization: competitiveness and competitive spirit.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind de volgende waarden binnen een organisatie belangrijk: behulpzaamheid en collegialiteit.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve que les valeurs suivantes sont importantes pour une organisation: la serviabilité et la collégialité.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find the following values are important to an organization: helpfulness and collegiality.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat er in een organisatie maatschappelijk engagement wordt getoond.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important qu’une organisation fasse preuve d’engagement civique.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I think it is important for an organization to demonstrate social engagement.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk te werken in een organisatie  waarin er aandacht is voor elk individu.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve  cela important de travailler dans une organisation où l’on porte une attention particulière à chaque individu.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization where one pays close attention to each person.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk  te kunnen werken in een organisatie waarin medewerkers trots zijn op hun bedrijf.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler dans une organisation où les employés sont fiers de leur entreprise.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization where employees are proud of their company.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat binnen een organisatie iedereen gelijkwaardig en fair behandeld wordt.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important que tout le monde au sein d''une organisation reçoive un traitement égal et équitable.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that everyone within an organization receives a fair and equal treatment.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat er binnen een organisatie ruimte is voor persoonlijke inbreng.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important qu''une organisation laisse la place à la contribution individuelle.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that an organization gives way to personal contribution.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk om mijn competenties op de werkvloer te kunnen ontplooien of de kans te krijgen opleidingen te volgen.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de pouvoir développer mes compétences au travail et avoir la possibilité de suivre des formations.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to develop my skills and to have the opportunity to attend trainings at work.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk om binnen mijn werkomgeving duidelijke instructies en structuur mee te krijgen.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important d’avoir une structure et des instructions claires dans mon environnement de travail.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to have a structure and clear direction in my work environment.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk voor de marktleider te werken of een organisatie met naamsbekendheid.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de pouvoir travailler pour une société leader du marché ou une société qui a une réputation bien établie.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work for a company that is market leader or a company that has an established reputation.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind dat een organisatie de lat hoog moet leggen voor zijn werknemers.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je pense que l''organisation doit placer la barre bien haut pour ses employés.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I think the organization has to set the bar pretty high for its employees.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk te werken voor een organisatie waarin betere prestaties meer beloond worden.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler pour une organisation qui récompense les bonnes performances.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work for an organization where good performance is rewarded.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind dat een organisatie maatregelen moet nemen zodat de werknemers hun werk  zo goed mogelijk met hun privéleven kunnen combineren.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je pense qu’une organisation doit prendre les mesures nécessaires pour permettre à leurs employés de combiner leur vie professionnelle et leur vie privée.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I think that an organization must take necessary measures to allow their employees to combine their professional and private life.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk goede contacten te hebben met mijn collega’s en leidinggevende.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important d’avoir de bonnes relations avec mes collègues et mes supérieurs.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to have good relations with colleagues and superiors.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat een organisatie zijn werknemers duidelijke regels oplegt.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important qu’une organisation fournisse des règles claires à ses employés.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that an organization provides clear rules to its employees.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat nieuwe werknemers of buitenstaanders zich snel welkom voelen binnen een organisatie.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important que les nouveaux employés se sentent vite intégrés et bien accueillis dans une entreprise.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that new employees feel welcomed and quickly integrated into a company.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk  te werken in een organisatie die proceduregericht werkt.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler dans une organisation qui suit et respecte des procédures.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization that respects and follows procedures.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat er een goede relatie is tussen een organisatie en zijn klanten.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important qu’il y ait une bonne relation entre l’organisation et ses clients.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to have a good relationship between the organization and its customers.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk om binnen een organisatie doorgroeimogelijkheden te hebben.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important d’avoir des possibilités de développement de  carrière au sein d’une organisation.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to have career development  opportunities within an organization.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk te werken in een organisatie waarin men de nodige ondersteuning kan krijgen.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler dans une organisation où l’on reçoit un soutien important.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization where one receives significant support.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat binnen een organisatie iedereen op een informele manier met elkaar omgaat.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important qu’au sein d’une organisation tout le monde se traite de manière informelle.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that in an organization everyone is treated informally.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk om formeel gekleed naar het werk te gaan.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important d’aller travailler avec une tenue formelle.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work with a formal outfit.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat mijn werkgever mijn job inhoudelijk afstemt op mijn persoonlijke interesses.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important que mon employeur aligne le contenu de mon emploi avec mes intérêts personnels.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important that my employer aligns the contents of my job with my personal interests.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk voor een organisatie te kunnen werken waarin jobzekerheid is.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de pouvoir travailler dans une organisation qui offre la sécurité de l’emploi.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization that offers job security.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat er in een organisatie ruimte is voor sociale activiteiten.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important d’avoir de l’espace pour les activités sociales au sein de l’organisation.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to have space for social activities within the organization.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk te werken voor een organisatie waarin er op een open manier gecommuniceerd wordt en waarin veel feedback gegeven wordt.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler dans une organisation où l’on communique ouvertement et où l’on reçoit du régulièrement un feedback.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work in an organization where one communicates openly and where one regularly receives feedback.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk te werken in een organisatie die zeer resultaatgericht is.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important de travailler pour une organisation qui est tourné vers les résultats.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important to work for an organization that is result-oriented.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Ik vind het belangrijk dat een organisatie dynamisch is en veranderingen durft doorvoeren.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Je trouve cela important qu’une organisation soit dynamique et qui ose les changements.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'I find it important for an organization to be dynamic and dare to change.', 0, @SystemUser, GETDATE(), 0,NEWID())

END


/*****************************************************
		FOR CUSTOMER
******************************************************/



SELECT @DefaultTheoremListTemplateId = NEWID()
SELECT @SystemUser = SYSTEM_USER

IF NOT EXISTS(SELECT * FROM [TheoremListTemplate] WHERE Name LIKE 'Template for companies')
BEGIN
INSERT INTO TheoremListTemplate (Id, Name, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@DefaultTheoremListTemplateId, 'Template for companies', @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Binnen onze organisatie heerst er een hiërarchische structuur.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Au sein de notre organisation, il existe une structure hiérarchique.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Within our organization there is a hierarchical structure.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Binnen onze organisatie genieten de werknemers van een zekere onafhankelijkheid en vrijheid.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Au sein de notre organisation, les employés jouissent d''une liberté et d''une indépendance.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Within our organization, employees enjoy freedom and independence.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Binnen onze organisatie geldt de stelling "Eén voor allen, allen voor één".', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Au sein de notre organisation l’expression «Un pour tous et tous pour un» est d’application.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Within our organization the expression "One for all and all for one" is a reality.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie zijn volgende waarden belangrijk: competitiviteit en prestatiedrang.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation les valeurs suivantes sont importantes : la compétitivité et l''esprit de compétition.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization the following values are important: competitiveness and competitive spirit.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie zijn volgende waarden belangrijk: behulpzaamheid en collegialiteit.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation ces valeurs sont importantes: la serviabilité et la collégialité.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'These values  are important to our organization: helpfulness and collegiality.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie toont veel maatschappelijk engagement.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation fait preuve d’engagement civique.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization demonstrates social engagement.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie wordt er aandacht besteed aan elk individu.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation, une attention particulière est accordée à chaque individu.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization, special attention is given to each person.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze medewerkers zijn over het algemeen trots te werken voor onze organisatie.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre personnel est généralement fier de travailler pour notre organisation.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our staff is generally proud to work for our organization.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Binnen onze organisatie wordt elke werknemer gelijkwaardig en fair behandeld.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Au sein de notre organisation, chaque employé est traité de manière égale et équitable.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Within our organization, every employee is treated fairly and equally.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie is er ruimte voor een persoonlijke inbreng.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation il y a un espace pour l’apport individuel.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization there is a space for individual input.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie wordt er aan werknemers de mogelijkheid geboden opleidingen te volgen of worden werknemers uitgedaagd zichzelf te ontplooien.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation les employés ont la possibilité de suivre des formations et ils sont encouragés à se développer.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization, employees have the opportunity to attend trainings and are encouraged to develop themselves.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie worden er duidelijke instructies en structuur meegegeven aan de medewerkers.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation nous fournissons une structure et des instructions claires à nos collaborateurs.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization we provide a structure and clear instructions to our employees.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie is marktleider of heeft een zekere naambekendheid.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation est leader du marché et a une réputation bien établie.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization is a market leader and has an established reputation.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie legt de lat hoog voor de werknemers.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation place la barre bien haut pour nos employés.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization sets the bar pretty high for our employees.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie worden betere prestaties meer beloond.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation les bonnes performances sont bien récompensées.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization good performance is  rewarded.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie neemt maatregelen zodat werknemers hun werkleven zo goed mogelijk kunnen combineren met hun privéleven.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation prend les mesures nécessaires pour permettre à ses employés de combiner leur vie professionnelle et leur vie privée.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization takes the necessary steps to allow its employees to combine their professional and private life.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie hecht belang aan goede contacten op de werkvloer.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation accorde de l’importance à la bonne entente au travail.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization attaches importance to good relations at work.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie legt zijn werknemers duidelijke regels op.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation fournit des règles claires à ses employés.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization provides clear rules to its employees.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie kijkt erop  toe dat nieuwe werknemers  of buitenstaanders zich snel welkom voelen.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation tâche de faire en sorte que les nouveaux employés se sentent rapidement intégrés et bien accueillis.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization ensures that new employees feel welcomed and quickly integrated.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie werkt proceduregericht.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation travaille sur base de procédures bien établies.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization works on the basis of established procedures.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie hecht veel belang aan relatie met de klanten.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation accorde beaucoup d’importance aux relations avec ses clients.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization places great importance to relations with its customers.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie biedt zijn werknemers doorgroeimogelijkheden aan.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation offre à ses employés des possibilités de développement de carrière.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization offers its employees  career development opportunities.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie biedt ondersteuning aan werknemers indien gewenst.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation offre un soutien aux employés qui le souhaitent.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization offers support to its employees.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie wordt er op een informele manier met elkaar omgesprongen.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation les personnes interagissent de manière informelle.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization people interact in an informal way.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie wordt er verwacht dat de werknemers formele kledij dragen.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation on attend à ce que nos collaborateurs viennent travailler avec une tenue formelle.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization we expect that our employees come to work with a formal outfit.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie wordt het takenpakket van de werknemers afgestemd op ieder zijn interesseveld indien mogelijk.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation les responsabilités des employés sont alignés, si possible, avec leur champ d’intérêts.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization the responsibilities of employees are aligned, if possible, with their field of interest.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie is er jobzekerheid.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation il y a une  sécurité de l’emploi.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization there is a job security.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'In onze organisatie is er ruimte voor sociale activiteiten.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation il y a un espace consacré aux activités sociales.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization there is  space for social activities.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie geeft veel feedback en heeft een open manier van communiceren.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Dans notre organisation on donne beaucoup de feedback et on pratique une communication ouverte.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'In our organization we give a lot of feedback and we practice open communication.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie is zeer resultaatgericht.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation est tournée vers les résultats.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization is result-oriented.', 0, @SystemUser, GETDATE(), 0,NEWID())

SELECT @TempTheoremTemplateId = NEWID()
INSERT INTO TheoremTemplate (Id, TheoremListTemplateId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES (@TempTheoremTemplateId, @DefaultTheoremListTemplateId, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 1, 'Onze organisatie is dynamisch en durft veranderingen doorvoeren.', 1, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 2, 'Notre organisation est dynamique et ose les changements.', 0, @SystemUser, GETDATE(), 0,NEWID())
INSERT INTO TheoremTemplateTranslation(Id, TheoremTemplateId, LanguageId, Text, IsDefault, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) VALUES(NEWID(), @TempTheoremTemplateId, 3, 'Our organization is dynamic and dare to change.', 0, @SystemUser, GETDATE(), 0,NEWID())

END
