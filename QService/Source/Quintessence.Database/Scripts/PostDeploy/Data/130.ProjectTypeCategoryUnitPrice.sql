/*ProjectTypeCategoryLevel*/
DECLARE @Level1 UNIQUEIDENTIFIER = NEWID()
DECLARE @Level2 UNIQUEIDENTIFIER = NEWID()
DECLARE @Level3 UNIQUEIDENTIFIER = NEWID()
DECLARE @Level4 UNIQUEIDENTIFIER = NEWID()
DECLARE @Level5 UNIQUEIDENTIFIER = NEWID()
DECLARE @Level6 UNIQUEIDENTIFIER = NEWID()

INSERT INTO ProjectTypeCategoryLevel (Id, Name) VALUES (@Level1, 'Directie en hoger')
INSERT INTO ProjectTypeCategoryLevel (Id, Name) VALUES (@Level2, 'Management')
INSERT INTO ProjectTypeCategoryLevel (Id, Name) VALUES (@Level3, 'Expert')
INSERT INTO ProjectTypeCategoryLevel (Id, Name) VALUES (@Level4, 'Lager management')
INSERT INTO ProjectTypeCategoryLevel (Id, Name) VALUES (@Level5, 'Operationeel LG')
INSERT INTO ProjectTypeCategoryLevel (Id, Name) VALUES (@Level6, 'Uitvoerend/administr')

/*Retrieve projectTypeCategories to combine*/
DECLARE @AcProjectTypeCategoryId UNIQUEIDENTIFIER
DECLARE @FaProjectTypeCategoryId UNIQUEIDENTIFIER
DECLARE @SoProjectTypeCategoryId UNIQUEIDENTIFIER
DECLARE @EaProjectTypeCategoryId UNIQUEIDENTIFIER

SELECT @AcProjectTypeCategoryId = Id FROM ProjectTypeCategoryView WHERE Code = 'AC'
SELECT @FaProjectTypeCategoryId = Id FROM ProjectTypeCategoryView WHERE Code = 'FA'
SELECT @SoProjectTypeCategoryId = Id FROM ProjectTypeCategoryView WHERE Code = 'SO'
SELECT @EaProjectTypeCategoryId = Id FROM ProjectTypeCategoryView WHERE Code = 'EA'

/*Insert unit price for AC per level*/
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @AcProjectTypeCategoryId, @Level1, 1973)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @AcProjectTypeCategoryId, @Level2, 1758)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @AcProjectTypeCategoryId, @Level3, 1599)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @AcProjectTypeCategoryId, @Level4, 1599)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @AcProjectTypeCategoryId, @Level5, 1599)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @AcProjectTypeCategoryId, @Level6, 1599)

/*Insert unit price for FA per level*/
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @FaProjectTypeCategoryId, @Level3, 1015)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @FaProjectTypeCategoryId, @Level4, 1015)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @FaProjectTypeCategoryId, @Level5, 907)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @FaProjectTypeCategoryId, @Level6, 907)

/*Insert unit price for SO per level*/
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @SoProjectTypeCategoryId, @Level3, 692)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @SoProjectTypeCategoryId, @Level4, 692)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @SoProjectTypeCategoryId, @Level5, 482)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @SoProjectTypeCategoryId, @Level6, 482)

/*Insert unit price for EA per level*/
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @EaProjectTypeCategoryId, @Level1, 2563)
INSERT INTO ProjectTypeCategoryUnitPrice(Id, ProjectTypeCategoryId, ProjectTypeCategoryLevelId, UnitPrice) VALUES (NEWID(), @EaProjectTypeCategoryId, @Level2, 2563)
