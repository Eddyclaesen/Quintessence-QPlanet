--DECLARE @ProjectTemp AS Table(
--	Id UNIQUEIDENTIFIER, 
--	FirstName NVARCHAR(MAX), 
--	LastName NVARCHAR(MAX),  
--	Email NVARCHAR(MAX), 
--	ActId INT)

--INSERT INTO @ProjectTemp
--SELECT  NEWID() AS Id,
--		ki.Voornaam AS FirstName,
--		Ki.Naam AS LastName,
--		ki.Email AS Email,
--		ki.Kand_Inf_ID AS ActId
--FROM [$(superoffice7server)].[$(SuperOffice7)].dbo.Kand_Inf ki
--WHERE ki.Voornaam IS NOT NULL

--INSERT INTO Candidate
--(Id, FirstName, LastName, Email)
--SELECT Id, FirstName, LastName, Email
--FROM @ProjectTemp

--INSERT INTO CandidateHistory
--SELECT Id, ActId 
--FROM @ProjectTemp