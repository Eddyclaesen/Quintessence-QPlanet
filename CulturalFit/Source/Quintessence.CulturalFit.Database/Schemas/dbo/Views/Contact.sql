CREATE VIEW [dbo].[Contact]
AS
SELECT     CONTACT_1.contact_id AS Id, CONTACT_1.name, CONTACT_1.department, CONTACT_1.associate_id AS AssociateId, cs.long08 AS AccountManagerAssociateId, 
                      ka.Klantassistente AS CustomerAssistenceAssociateId
FROM         [$(SuperOffice7Server)].[$(SuperOffice7)].dbo.CONTACT AS CONTACT_1 LEFT OUTER JOIN
                     [$(SuperOffice7Server)].[$(SuperOffice7)].dbo.UDCONTACTSMALL AS cs ON cs.udcontactSmall_id = CONTACT_1.contact_id LEFT OUTER JOIN
                     [$(SuperOffice7Server)].[$(SuperOffice7)].dbo.vw_Klanten_ptool AS ka ON ka.[ID Klant] = CONTACT_1.contact_id
WHERE     (CONTACT_1.deleted IS NULL OR CONTACT_1.deleted = 0)