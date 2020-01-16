EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'QUINTDOMAIN\RSUser';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'QUINTDOMAIN\RSUser';


GO
EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'QuintessenceUser';

