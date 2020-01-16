GO

CREATE FUNCTION dbo.splitstring ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE LEN(@stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(';', @stringToSplit)


if @pos = 0
        SELECT @pos = LEN(@stringToSplit)


  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 RETURN
END
GO

INSERT INTO [ProjectDna] ([Id], [CrmProjectId], [Details], [ApprovedForReferences])
	SELECT		NEWID(), ProjectFicheInformatie.Project_ID, ProjectFicheInformatie.Details, CASE WHEN ProjectFicheInformatie.GeenReferentie = 1 THEN 0 ELSE 1 END
	FROM		[$(superoffice7server)].[$(SuperOffice7)].[dbo].[ProjectFicheInformatie]

INSERT INTO [ProjectDnaCommercialTranslation]([Id], [ProjectDnaId], [LanguageId], [CommercialName], [CommercialRecap])
	SELECT		NEWID(), [Id], 1, '', [ProjectFicheInformatie].[SamenVatting]
	FROM		[$(superoffice7server)].[$(SuperOffice7)].[dbo].[ProjectFicheInformatie]
	INNER JOIN	[ProjectDnaView]
		ON		[ProjectDnaView].[CrmProjectId] = [ProjectFicheInformatie].[Project_Id]

INSERT INTO [ProjectDnaCommercialTranslation]([Id], [ProjectDnaId], [LanguageId], [CommercialName], [CommercialRecap])
	SELECT		NEWID(), [Id], 2, '', [ProjectFicheInformatie].[SamenVattingFR]
	FROM		[$(superoffice7server)].[$(SuperOffice7)].[dbo].[ProjectFicheInformatie]
	INNER JOIN	[ProjectDnaView]
		ON		[ProjectDnaView].[CrmProjectId] = [ProjectFicheInformatie].[Project_Id]

INSERT INTO [ProjectDnaCommercialTranslation]([Id], [ProjectDnaId], [LanguageId], [CommercialName], [CommercialRecap])
	SELECT		NEWID(), [Id], 3, '', [ProjectFicheInformatie].[SamenVattingEN]
	FROM		[$(superoffice7server)].[$(SuperOffice7)].[dbo].[ProjectFicheInformatie]
	INNER JOIN	[ProjectDnaView]
		ON		[ProjectDnaView].[CrmProjectId] = [ProjectFicheInformatie].[Project_Id]

DECLARE @pk AS NVARCHAR(MAX)
DECLARE @ProjectId AS INT

DECLARE curName CURSOR FOR SELECT ContactPersonen, Project_id FROM [$(superoffice7server)].[$(SuperOffice7)].[dbo].[ProjectFicheInformatie]
OPEN curName 

FETCH NEXT FROM curName INTO @pk, @ProjectId

WHILE @@FETCH_STATUS = 0
BEGIN	
	INSERT INTO [ProjectDna2CrmPerson]([Id], [ProjectDnaId], [CrmPersonId])
		SELECT NEWID(), [ProjectDnaView].[Id],	CAST(Name AS INT) FROM dbo.splitstring(replace(@pk, 'X', ''))
		INNER JOIN	[ProjectDnaView]
			ON		[ProjectDnaView].[CrmProjectId] = @ProjectId
		WHERE	CAST(Name AS INT) <> 0

    --insert code here
    FETCH NEXT FROM curName INTO @pk, @ProjectId --do not forget this line.. will cause an infinite loop
END
      
CLOSE curName 
DEALLOCATE curName


GO

DROP FUNCTION dbo.splitstring
GO

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D06'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long06] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D07'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long07] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D08'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long08] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D09'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long09] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D10'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long10] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D11'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long11] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D12'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long12] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D13'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long13] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D14'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long14] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D15'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long15] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D16'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long16] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D17'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long17] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D18'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long18] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D19'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long19] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D20'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long20] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D21'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long21] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D22'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long22] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D23'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long23] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D24'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long24] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D25'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long25] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D26'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long26] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D27'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long27] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D28'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long28] = 1

INSERT INTO [ProjectDna2ProjectDnaType]([Id], [ProjectDnaId], [ProjectDnaTypeId])
	SELECT		NEWID(), [ProjectDnaView].[Id], 'D1D72438-7069-49C5-83D9-2D236FD41D29'
	FROM		[ProjectDnaView]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[Project] [CrmProject]
		ON		[CrmProject].[project_id] = [ProjectDnaView].[CrmProjectId]
	INNER JOIN	[$(superoffice7server)].[$(SuperOffice7)].[dbo].[UDPROJECTSMALL] [CrmProjectType]
		ON		[CrmProjectType].[udprojectsmall_id] = [CrmProject].[userdef_id]
	WHERE		[CrmProjectType].[long29] = 1