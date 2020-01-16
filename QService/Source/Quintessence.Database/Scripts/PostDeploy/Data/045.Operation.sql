--DECLARE @DimOperationDomainId AS UNIQUEIDENTIFIER = NEWID()
--DECLARE @SecOperationDomainId AS UNIQUEIDENTIFIER = NEWID()
--DECLARE @PrmOperationDomainId AS UNIQUEIDENTIFIER = NEWID()
--DECLARE @SimOperationDomainId AS UNIQUEIDENTIFIER = NEWID()

----DIM Domain
-------------
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @DimOperationDomainId, 'DIMLIQUINT', 'List Quintessence Dictionaries', 'Retrieve a list of all the Quintessence dictionaries')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @DimOperationDomainId, 'DIMSEARCH', 'Search Customer Dictionaries', 'Retrieve a list of dictionaries by search terms')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @DimOperationDomainId, 'DIMDETAILS', 'Search Customer Dictionaries', 'Retrieve a list of dictionaries by search terms')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @DimOperationDomainId, 'DIMCREATE', 'Create new Dictionary', 'Create a new dictionary')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @DimOperationDomainId, 'DIMUPDATE', 'Update a Dictionary', 'Modify information of a dictionary')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @DimOperationDomainId, 'DIMDELETE', 'Delete a Dictionary', 'Delete a dictionary')

----SEC Domain
-------------
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SecOperationDomainId, 'ADMINREVOK', 'Revoke all authenticationcode', 'Revoke all valid authentication codes to force the user to re-authenticate him/herself')

----PRM Domain
-------------
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @PrmOperationDomainId, 'PRMALLMYPR', 'List all projects of current user', 'Retrieves a list of all the projects of the logged in user')

----SIM Domain
-------------
--INSERT INTO [OperationDomain] (Id, Code, Name, Description) VALUES (@SimOperationDomainId, 'SIM', 'Simulation Management Domain', 'Contains all the operations related to Simulation Management')

--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMLISTSET', 'List Simulation sets', 'Retrieve a list of all the simulation sets')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMLISTDEP', 'List Simulation departments', 'Retrieve a list of all the simulation departments')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMLISTLEV', 'List Simulation levels', 'Retrieve a list of all the simulation levels')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMLISTULA', 'List Simulations', 'Retrieve a list of all the simulations')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMLISTCOM', 'List Simulation combinations', 'Retrieve a list of all the simulation combinations')

--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMRETRSET', 'Retrieve a Simulation set', 'Retrieve the detail of a simulation set')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMRETRDEP', 'Retrieve a Simulation department', 'Retrieve the detail of a simulation department')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMRETRLEV', 'Retrieve a Simulation level', 'Retrieve the detail of a simulation level')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMRETRULA', 'Retrieve a Simulation', 'Retrieve a detail of a simulation')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMRETRCOM', 'Retrieve a Simulation combination', 'Retrieve the detail of a simulation combination')

--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMDELESET', 'Delete a Simulation set', 'Delete a simulation set')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMDELEDEP', 'Delete a Simulation department', 'Delete a simulation department')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMDELELEV', 'Delete a Simulation level', 'Delete a simulation level')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMDELEULA', 'Delete a Simulation', 'Delete a simulation')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMDELECOM', 'Delete a Simulation combination', 'Delete a simulation combination')

--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMUPDASET', 'Update a Simulation set', 'Update a simulation set')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMUPDADEP', 'Update a Simulation department', 'Update a simulation department')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMUPDALEV', 'Update a Simulation level', 'Update a simulation level')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMUPDAULA', 'Update a Simulation', 'Update a simulation')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMUPDACOM', 'Update a Simulation combination', 'Update a simulation combination')

--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMCREASET', 'Create a Simulation set', 'Create a simulation set')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMCREADEP', 'Create a Simulation department', 'Create a simulation department')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMCREALEV', 'Create a Simulation level', 'Create a simulation level')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMCREAULA', 'Create a Simulation', 'Create a simulation')
--INSERT INTO [Operation] (Id, OperationDomainId, Code, Name, Description) VALUES (NEWID(), @SimOperationDomainId, 'SIMCREACOM', 'Create a Simulation combination', 'Create a simulation combination')