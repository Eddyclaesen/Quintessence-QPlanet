--CREATE LOGIN [CulturalFitUser] WITH PASSWORD = '$Quint123'
--GO

--CREATE USER [CulturalFitUser] FOR LOGIN [CulturalFitUser]
--GO

--EXEC sp_addrolemember N'db_owner', N'CulturalFitUser'
--GO

--CREATE USER [QUINTDOMAIN\RSUser] FOR LOGIN [QUINTDOMAIN\RSUser]
--GO

--EXEC sp_addrolemember N'db_owner', N'QUINTDOMAIN\RSUser'
--GO

