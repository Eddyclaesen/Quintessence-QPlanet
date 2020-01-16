IF suser_sname() <> 'QuintessenceUser' AND NOT EXISTS(SELECT * FROM master.dbo.syslogins WHERE name = 'QuintessenceUser')
BEGIN
	CREATE LOGIN [QuintessenceUser] WITH PASSWORD = '$Quint123'	
END
GO

IF suser_sname() <> 'QuintessenceUser'
BEGIN
	CREATE USER [QuintessenceUser] FOR LOGIN [QuintessenceUser]
END
GO

IF suser_sname() <> 'QuintessenceUser'
BEGIN
	EXEC sp_addrolemember N'db_owner', N'QuintessenceUser'
END
GO

:r .\Data\000.Configurations.sql
:r .\Data\001.Replication.sql
:r .\Data\002.Language.sql
:r .\Data\005.Declarations.sql
:r .\Data\010.ProjectType.sql
:r .\Data\012.TimesheetEntryStatus.sql
:r .\Data\020.Category.sql
:r .\Data\025.SurveyPlanning.sql
:r .\Data\041.Office.sql
:r .\Data\043.ReportStatus.sql
:r .\Data\045.Operation.sql
:r .\Data\046.Role.sql
:r .\Data\047.PricingModel.sql
:r .\Data\050.User.sql
--:r .\Data\060.Dictionary.sql
:r .\Data\060.Dictionary_027.sql
:r .\Data\060.Dictionary_033.sql
:r .\Data\060.Dictionary_041.sql
:r .\Data\060.Dictionary_060.sql
:r .\Data\060.Dictionary_072.sql
:r .\Data\060.Dictionary_073.sql
:r .\Data\060.Dictionary_075.sql
:r .\Data\060.Dictionary_078.sql
:r .\Data\060.Dictionary_087.sql
:r .\Data\060.Dictionary_088.sql
:r .\Data\060.Dictionary_089.sql
:r .\Data\060.Dictionary_102.sql
:r .\Data\060.Dictionary_104.sql
:r .\Data\060.Dictionary_109.sql
:r .\Data\060.Dictionary_116.sql
:r .\Data\060.Dictionary_117.sql
:r .\Data\060.Dictionary_118.sql
:r .\Data\060.Dictionary_121.sql
:r .\Data\060.Dictionary_122.sql
:r .\Data\060.Dictionary_125.sql
:r .\Data\060.Dictionary_144.sql
:r .\Data\060.Dictionary_152.sql
:r .\Data\060.Dictionary_158.sql
:r .\Data\060.Dictionary_159.sql
:r .\Data\060.Dictionary_160.sql
:r .\Data\060.Dictionary_169.sql
:r .\Data\060.Dictionary_173.sql
:r .\Data\060.Dictionary_177.sql
:r .\Data\060.Dictionary_178.sql
:r .\Data\070.SimulationSet.sql
:r .\Data\080.Report.sql
--:r .\Data\100.Project.AC.sql
--:r .\Data\100.Project.DC.sql
--:r .\Data\100.Project.EA.sql
--:r .\Data\100.Project.FD.sql
:r .\Data\109.CrmProject.sql
:r .\Data\110.ContactDetail.sql
:r .\Data\120.ProjectRole.sql
:r .\Data\130.ProjectTypeCategoryUnitPrice.sql
:r .\Data\150.Candidate.sql
:r .\Data\160.ProgramComponentSpecial.sql
:r .\Data\200.ActivityType.sql
:r .\Data\220.ProductType.sql
:r .\Data\230.MailStatusType.sql
:r .\Data\240.EvaluationFormType.sql
:r .\Data\250.ComplaintType.sql
:r .\Data\251.ComplaintStatusType.sql
:r .\Data\252.ComplaintSeverityType.sql
:r .\Data\260.MailTemplate.sql
:r .\Data\270.TheoremListRequestType.sql
:r .\Data\271.TheoremListTemplate.sql
:r .\Data\272.TheoremListType.sql
:r .\Data\300.ProjectDna.sql
:r .\Data\999.Jobs.sql