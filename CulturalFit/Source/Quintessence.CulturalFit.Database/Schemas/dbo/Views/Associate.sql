CREATE VIEW [dbo].[Associate]
AS
SELECT		a.associate_id AS Id, 
			a.name AS UserName, 
			p.firstname, 
			p.lastname, 
			e.email_address AS Email,
			a.group_idx AS UserGroupId
FROM        [$(SuperOffice7Server)].[$(SuperOffice7)].dbo.ASSOCIATE AS a
	INNER JOIN [$(SuperOffice7Server)].[$(SuperOffice7)].dbo.PERSON AS p 
		ON p.person_id = a.person_id
	INNER JOIN [$(SuperOffice7Server)].[$(SuperOffice7)].dbo.EMAIL AS e
		ON p.person_id = e.person_id
WHERE     (a.deleted IS NULL OR a.deleted = 0)
	AND e.email_address LIKE '%quintessence.be%'