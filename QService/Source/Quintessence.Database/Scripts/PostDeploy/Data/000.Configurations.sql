EXEC [CrmReplicationAppointment_Sync]
EXEC [CrmReplicationAppointmentTimesheet_Sync]
EXEC [CrmReplicationAppointmentTraining_Sync]
EXEC [CrmReplicationAssociate_Sync]
EXEC [CrmReplicationContact_Sync]
EXEC [CrmReplicationEmail_Sync]
EXEC [CrmReplicationEmailAssociate_Sync]
EXEC [CrmReplicationPerson_Sync]
EXEC [CrmReplicationProject_Sync]

--EXEC master.dbo.sp_droplinkedsrvlogin @rmtsrvname = N'SUPEROFFICE7SERVER', @locallogin = N'QUINTDOMAIN\sve'
--EXEC master.dbo.sp_droplinkedsrvlogin @rmtsrvname = N'SUPEROFFICE7SERVER', @locallogin = N'QUINTDOMAIN\tking'
--EXEC master.dbo.sp_droplinkedsrvlogin @rmtsrvname = N'SUPEROFFICE7SERVER', @locallogin = N'QUINTDOMAIN\ceddy'
--EXEC master.dbo.sp_droplinkedsrvlogin @rmtsrvname = N'SUPEROFFICE7SERVER', @locallogin = N'QuintessenceUser'
--EXEC master.dbo.sp_droplinkedsrvlogin @rmtsrvname = N'SUPEROFFICE7SERVER', @locallogin = N'CulturalFitUser'
--GO
--
--EXEC master.dbo.sp_droplinkedsrvlogin @rmtsrvname = N'SUPEROFFICE7SERVER', @locallogin = NULL
--GO
--
--EXEC master.dbo.sp_dropserver @server = N'SUPEROFFICE7SERVER'
--GO

--EXEC master.dbo.sp_addlinkedserver @server = N'SUPEROFFICE7SERVER', @srvproduct=N'vm-quintsql', @provider=N'SQLNCLI', @datasrc=N'vm-quintsql', @catalog=N'Superoffice7'
--GO
-- 
--EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SUPEROFFICE7SERVER',@useself=N'True',@locallogin=N'QUINTDOMAIN\sve',@rmtuser=NULL,@rmtpassword=NULL
--EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SUPEROFFICE7SERVER',@useself=N'True',@locallogin=N'QUINTDOMAIN\tking',@rmtuser=NULL,@rmtpassword=NULL
--EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SUPEROFFICE7SERVER',@useself=N'True',@locallogin=N'QUINTDOMAIN\ceddy',@rmtuser=NULL,@rmtpassword=NULL
--
--EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SUPEROFFICE7SERVER',@useself=N'False',@locallogin=N'QuintessenceUser',@rmtuser=N'QuintessenceUser',@rmtpassword='$Quint123'
--EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SUPEROFFICE7SERVER',@useself=N'False',@locallogin=N'CulturalFitUser',@rmtuser=N'CulturalFitUser',@rmtpassword='$Quint123'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'collation compatible', @optvalue=N'false'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'data access', @optvalue=N'true'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'dist', @optvalue=N'false'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'pub', @optvalue=N'false'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'rpc', @optvalue=N'false'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'rpc out', @optvalue=N'true'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'sub', @optvalue=N'false'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'connect timeout', @optvalue=N'0'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'collation name', @optvalue=null
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'lazy schema validation', @optvalue=N'false'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'query timeout', @optvalue=N'0'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'use remote collation', @optvalue=N'true'
--GO
--
--EXEC master.dbo.sp_serveroption @server=N'SUPEROFFICE7SERVER', @optname=N'remote proc transaction promotion', @optvalue=N'true'
--GO


