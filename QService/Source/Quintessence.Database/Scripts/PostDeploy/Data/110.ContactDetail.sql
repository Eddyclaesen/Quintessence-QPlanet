DECLARE @ProjectContactTable AS TABLE(ContactId INT)

INSERT INTO @ProjectContactTable(ContactId)
	SELECT	DISTINCT	
				[ProjectView].[ContactId]
	FROM		[ProjectView]

INSERT INTO ContactDetail(Id, ContactId, Remarks)
	SELECT	NEWID(), ContactId, NULL
	FROM	@ProjectContactTable