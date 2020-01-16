INSERT INTO [CandidateScoreReportType](Code, Name) VALUES ('CO', 'Competence')
INSERT INTO [CandidateScoreReportType](Code, Name) VALUES ('CL', 'Cluster')

INSERT INTO [Advice](Code, Name) VALUES ('NONE', 'None')
INSERT INTO [Advice](Code, Name) VALUES ('POSITIVE', 'Positive')
INSERT INTO [Advice](Code, Name) VALUES ('MODERATE', 'Moderately positive')
INSERT INTO [Advice](Code, Name) VALUES ('NEGATIVE', 'Negative')

INSERT INTO [ReportType](Code, Name) VALUES ('PROJECT', 'Project reports')
INSERT INTO [ReportType](Code, Name) VALUES ('CANDIDATE', 'Candidate reports')
INSERT INTO [ReportType](Code, Name) VALUES ('USER', 'User reports')

DECLARE @ReportAssessmentCenterId AS UNIQUEIDENTIFIER = NEWID()
INSERT INTO [CandidateReportDefinition]([Id], [Name], [Location], [IsActive]) VALUES (@ReportAssessmentCenterId, 'Default_AC++', '/QPlanet/AcDcReporting/ReportAssessmentCenter', 1)
INSERT INTO [CandidateReportDefinitionField]([Id], [CandidateReportDefinitionId], [Code], [Name], [IsActive]) VALUES (NEWID(), @ReportAssessmentCenterId, 'MOTIV', 'Motivation', 1)
INSERT INTO [CandidateReportDefinitionField]([Id], [CandidateReportDefinitionId], [Code], [Name], [IsActive]) VALUES (NEWID(), @ReportAssessmentCenterId, 'ORGFIT', 'Organisational Fit', 1)
INSERT INTO [CandidateReportDefinitionField]([Id], [CandidateReportDefinitionId], [Code], [Name], [IsActive]) VALUES (NEWID(), @ReportAssessmentCenterId, 'PERSONALITY', 'Characteristics of Personality', 1)

DECLARE @ReportFocusedAssessmentId AS UNIQUEIDENTIFIER = NEWID()
INSERT INTO [CandidateReportDefinition]([Id], [Name], [Location], [IsActive]) VALUES (@ReportFocusedAssessmentId, 'Default_FA++', '/QPlanet/AcDcReporting/ReportFocusedAssessment', 1)

DECLARE @ReportDevelopmentCenterId AS UNIQUEIDENTIFIER = NEWID()
INSERT INTO [CandidateReportDefinition]([Id], [Name], [Location], [IsActive]) VALUES (@ReportDevelopmentCenterId, 'Default_DC++', '/QPlanet/AcDcReporting/ReportDevelopmentCenter', 1)
INSERT INTO [CandidateReportDefinitionField]([Id], [CandidateReportDefinitionId], [Code], [Name], [IsActive]) VALUES (NEWID(), @ReportDevelopmentCenterId, 'PERSONALITY', 'Characteristics of Personality', 1)

INSERT INTO [ReportDefinition]([Id], [ReportTypeId], [Name], [IsActive], [Location], [ExportAsPdf], [ExportAsExcel], [ExportAsWord]) VALUES (NEWID(), 1, 'Competence Profile (NL)', 1, '/QPlanet/ProjectReporting/ReportingProjectExportCompetenceProfile_Dutch', 1, 1, 1)
INSERT INTO [ReportDefinition]([Id], [ReportTypeId], [Name], [IsActive], [Location], [ExportAsPdf], [ExportAsExcel], [ExportAsWord]) VALUES (NEWID(), 1, 'Competence Profile (FR)', 1, '/QPlanet/ProjectReporting/ReportingProjectExportCompetenceProfile_French', 1, 1, 1)
INSERT INTO [ReportDefinition]([Id], [ReportTypeId], [Name], [IsActive], [Location], [ExportAsPdf], [ExportAsExcel], [ExportAsWord]) VALUES (NEWID(), 1, 'Competence Profile (EN)', 1, '/QPlanet/ProjectReporting/ReportingProjectExportCompetenceProfile_English', 1, 1, 1)

INSERT INTO [ReportDefinition]([Id], [ReportTypeId], [Name], [IsActive], [Location], [ExportAsPdf], [ExportAsExcel], [ExportAsWord]) VALUES (NEWID(), 1, 'Competence Profile zonder Indicatoren (NL)', 1, '/QPlanet/ProjectReporting/ReportingProjectExportCompetenceProfileWithoutIndicators_Dutch', 1, 1, 1)
INSERT INTO [ReportDefinition]([Id], [ReportTypeId], [Name], [IsActive], [Location], [ExportAsPdf], [ExportAsExcel], [ExportAsWord]) VALUES (NEWID(), 1, 'Competence Profile zonder Indicatoren (FR)', 1, '/QPlanet/ProjectReporting/ReportingProjectExportCompetenceProfileWithoutIndicators_French', 1, 1, 1)
INSERT INTO [ReportDefinition]([Id], [ReportTypeId], [Name], [IsActive], [Location], [ExportAsPdf], [ExportAsExcel], [ExportAsWord]) VALUES (NEWID(), 1, 'Competence Profile zonder Indicatoren (EN)', 1, '/QPlanet/ProjectReporting/ReportingProjectExportCompetenceProfileWithoutIndicators_English', 1, 1, 1)