DECLARE @MailTemplateId UNIQUEIDENTIFIER

/**************************************
**** Candidate invitation template ****
***************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'CandidateInvitation', 'CANDINVITE', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListProjectCandidateMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Uitnodiging', '', 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Invitation', '', 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Invitation', '', 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Einladung', '', 4, @MailTemplateId)

/**************************************
****Category detail type 1 template****
***************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'CategoryDetailType1', 'DETAILTYPE1', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListProjectCandidateCategoryDetailType1MailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 4, @MailTemplateId)

/**************************************
****Category detail type 3 template****
***************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'CategoryDetailType3', 'DETAILTYPE3', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListProjectCandidateCategoryDetailType3MailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 4, @MailTemplateId)

/***************************************************
**** Cultural fit candidate invitation template ****
****************************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'CandidateCulturalFitInvitation', 'CANDCULTFIT', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListProjectCandidateCulturalFitMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 4, @MailTemplateId)

/**************************************************
**** Cultural fit customer invitation template ****
***************************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'CustomerCulturalFitInvitation', 'CUSTCULTFIT', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListCulturalFitMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Uitnodiging', '', 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Invitation', '', 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Invitation', '', 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [Subject], [Body], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 'Einladung', '', 4, @MailTemplateId)

/***********************************************
****   Evaluation form coaching template    ****
************************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'EvaluationFormCoaching', 'EVALCOACH', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListEvaluationFormMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 4, @MailTemplateId)

/*******************************************
****   Evaluation form ACDC template    ****
********************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'EvaluationFormAcdc', 'EVALACDC', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListEvaluationFormMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 4, @MailTemplateId)

/******************************************************
****   Evaluation form Custom projects template    ****
*******************************************************/

SET @MailTemplateId = NEWID()

INSERT INTO	[MailTemplate]([Id], [Name], [Code], [FromAddress], [BccAddress], [StoredProcedureName]) 
VALUES (@MailTemplateId, 'EvaluationFormCustomProjects', 'EVALCUSTOM', 'secretariaat@quintessence.be', 'secretariaat@quintessence.be', 'ListEvaluationFormMailTags')

--Dutch
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 1, @MailTemplateId)

--French
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 2, @MailTemplateId)

--English
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 3, @MailTemplateId)

--German
INSERT INTO [MailTemplateTranslation]([Id], [LanguageId], [MailTemplateId])
VALUES(NEWID(), 4, @MailTemplateId)