--ACDC
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Assessment center', 'AC', 167)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Focused assessment', 'FA', 168)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Development center', 'DC', 169)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Focused development', 'FD', 170)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Executive Assessment', 'EA', 171)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Psychologische screening', 'PS', 172)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Second opinion', 'SO', 173)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Custom with scoring', 'CA', 174)
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId) VALUES(NEWID(), 'B5D045', 'Custom', 'CU', 175)

INSERT INTO ProjectType2ProjectTypeCategory (ProjectTypeCategoryId, ProjectTypeId, IsMain)
	SELECT Id, @ACDC, 1 FROM ProjectTypeCategory WHERE Code IN ('AC', 'FA', 'DC', 'FD', 'EA', 'PS', 'SO', 'CA', 'CU')

----------------------------
--TYPE 1
----------------------------
DECLARE @CategoryId AS UNIQUEIDENTIFIER = NEWID()
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Leerstijlen van Kolb', 'LEERKOLB', 176, 1, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Leerstijlen van Kolb')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Style d''apprentissage de Kolb')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Learning style of Kolb')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Lernstil von Kolb')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Schein', 'SCHEIN', 177, 1, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Schein')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Schein')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Schein')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Schein')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'POP-gesprek', 'POPGESPREK', 178, 1, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'POP-gesprek')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Entretien de PDP')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'PDP interview')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'PEP-talk')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Interview', 'INTERVIEW', 179, 1, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Interview')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Entretien')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Interview')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Interview')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5A6D3', 'Reflectiegesprek', 'REFLECTGES', 180, 1, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Reflectiegesprek')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Interview réflexive')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Reflective interview')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Reflektierenden Interview')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5A6D3', 'Terugkoppelingsgesprek', 'FEEDBACGES', 181, 1, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Terugkoppelingsgesprek')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Interview réflexive')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Feedback interview')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Rückkopplung Interview')

----------------------------
--TYPE 2
----------------------------
SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'F4E790', 'Scope³', 'SCOPE3', 182, 2, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Scope³')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Scope³')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Scope³')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Scope³')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'F4E790', 'Web-In basket', 'WEBIB', 183, 2, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Web-In basket')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Web-In basket')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Web-In basket')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Web-In basket')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'CV-screening', 'CVSCREEN', 184, 2, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'CV-screening')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'CV-screening')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'CV-screening')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'CV-screening')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Perspective', 'PERSPECTIV', 185, 2, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Perspective')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Perspective')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Perspective')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Perspective')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Evolutips', 'EVOLUTIPS', 186, 2, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Evolutips')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Evolutips')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Evolutips')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Evolutips')

----------------------------
--TYPE 3
----------------------------
SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Reflectiedocument', 'REFLECTDOC', 187, 3, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Reflectiedocument')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Reflectiedocument')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Reflectiedocument')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Reflectiedocument')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'Leiderschapsstijl', 'LEIDERSTIJ', 188, 3, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'Leiderschapsstijl')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'Leiderschapsstijl')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'Leiderschapsstijl')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'Leiderschapsstijl')

SET @CategoryId = NEWID();
INSERT INTO ProjectTypeCategory(Id, Color, Name, Code, CrmTaskId, SubCategoryType, Execution) VALUES(@CategoryId, 'B5D045', 'NEO-pir', 'NEOPIR', 189, 3, 45)
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'SURVEYPLANNINGID', '1')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'DESCRIPTION', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTSTANDALONE', '<<default text>>')
INSERT INTO ProjectTypeCategoryDefaultValue(Id, ProjectTypeCategoryId, Code, Value) VALUES(NEWID(), @CategoryId, 'MAILTEXTINTEGRATED', '<<default text>>')
/*Translations*/
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 1, 'NEO-pir')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 2, 'NEO-pir')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 3, 'NEO-pir')
INSERT INTO ProjectTypeCategoryTranslation(Id, ProjectTypeCategoryId, LanguageId, Name) VALUES (NEWID(), @CategoryId, 4, 'NEO-pir')

--NOT PART OF SUBCATEGORIES ANY LONGER...
--INSERT INTO ProjectTypeCategory(Id, Name, Code) VALUES(NEWID(), 'Simulaties ontwikkelen', 'SIMONTW')

INSERT INTO ProjectType2ProjectTypeCategory (ProjectTypeCategoryId, ProjectTypeId, IsMain)
	SELECT Id, @ACDC, 0 FROM ProjectTypeCategory WHERE Code IN ('LEERKOLB', 'COACHING', 'SCHEIN', 'SCOPE3', 'WEBIB', 'CVSCREEN', 'PERSPECTIV', 'EVOLUTIPS', 'POPGESPREK', 'NEOPIR', 'INTERVIEW', 'REFLECTDOC', 'REFLECTGES', 'FEEDBACGES', 'SIMONTW', 'LEIDERSTIJ')

--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('11E4F53C-430B-4271-A435-25497DE8650A', 'Competentieprofielen', @CompetenceMgmt,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('D63D76FC-1CC9-4D74-8075-3F9C12BC49C4', 'Opleiding', @CompetenceMgmt,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('CAB84D7C-4CB9-4E99-BC34-B0FEB3C847BC', 'Competentiewoordenboek', @CompetenceMgmt,1)

--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('EF5AE42C-6939-4873-ADCD-1C8278C18330', 'CPC', @Training,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('E7CEC337-2534-481C-B41A-2D93754ADB7F', 'Medewerkers begeleiden in hun ontwikkeling', @Training,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('B37CAD07-0002-49EF-94AD-E06BBED4524A', 'Andere', @Training,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('2FBB0E3F-8CA6-48EC-8115-E70993C29320', 'Direct aan de slag met competenties', @Training,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('79DF74DA-D6D5-4FDE-A1F1-B090FD4239D3', 'CCA', @Training,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('C1142C31-417E-4673-BBB6-BFB2AF9F3FF0', 'GGI', @Training,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('00840804-BB7E-44A3-B730-C6B47A8B8D41', 'POP-workshop', @Training,1)

--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('05D7808F-EFDD-4EE6-82FD-217B27F70D76', 'Scope³', @Tools,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('EE3DC1E8-6896-4C9C-AED9-3CBFB6AB0386', 'Web-In-basket', @Tools,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('756F9727-BACF-4F06-B4CB-4E2D90EC2E51', 'Q-viewmaster', @Tools,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('D1C21D13-29E7-4A23-9D76-ACFD80FB68BF', 'Perspective', @Tools,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('DCFF6360-D027-4B73-97C6-CCE63E1D9B41', 'Evolutips', @Tools,1)
--INSERT INTO CategoryType(Id, Name, ProjectTypeId, IsCombinable) VALUES('3C18F17A-D932-412A-8CAA-DF9021DCF0B9', 'Competentiekaartjes', @Tools,1)