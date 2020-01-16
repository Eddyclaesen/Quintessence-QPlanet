CREATE VIEW [dbo].[Project]
	AS 
SELECT DISTINCT p.Project_ID AS Id, p.Omschrijving AS Name, pf.Proma_ID AS AssociateId, p.Contact_ID AS ContactId, pf.ACProject_ID AS AcProjectId
FROM         [$(ACTSERVER)].[$(ACT)].dbo.Projecten AS p INNER JOIN
                      [$(ACTSERVER)].[$(ACT)].dbo.ProjectFiche AS pf ON p.Project_ID = pf.Project_ID
WHERE     (p.deleted = 0)