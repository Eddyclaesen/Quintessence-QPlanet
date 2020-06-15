CREATE PROCEDURE [dbo].[CrmReplicationEmail_Sync]
AS
BEGIN
	DECLARE @JobHistoryId AS UNIQUEIDENTIFIER
	DECLARE @JobName AS NVARCHAR(MAX)

	SELECT	@JobName = OBJECT_NAME(@@PROCID)

	EXEC [dbo].[CrmReplicationJob_StartProcess] @JobName, @JobHistoryId OUTPUT

	DECLARE	@Email	AS	TABLE(								
								[Id]					INT			NOT NULL,
								[ContactId]				INT,
								[PersonId]				INT,
								[ContactName]			NVARCHAR(MAX),
								[FirstName]				NVARCHAR(MAX),
								[LastName]				NVARCHAR(MAX),
								[Email]					NVARCHAR(MAX),
								[DirectPhone]			NVARCHAR(MAX),
								[MobilePhone]			NVARCHAR(MAX))

	INSERT INTO @Email(	[Id],
							[ContactId],
							[PersonId],
							[ContactName],
							[FirstName],
							[LastName],
							[Email],
							[DirectPhone],
							[MobilePhone])
		SELECT		Email.email_id		AS Id,
					Person.contact_id	AS ContactId,
					Person.person_id	AS PersonId,
					Contact.name		AS ContactName,
					Person.firstname	AS FirstName,
					Person.Lastname		AS LastName,
					Email.email_address	AS Email,
					DirectPhone.phone	AS	DirectPhone,
					MobilePhone.phone	As	MobilePhone

		FROM		[Superoffice7].[CRM7].[email]		AS Email

		INNER JOIN	[Superoffice7].[CRM7].[Person]	AS Person
			ON		Person.person_id = Email.person_id

		INNER JOIN	[Superoffice7].[CRM7].[Contact]	AS Contact
			ON		Contact.contact_id = Person.contact_id
			
		LEFT JOIN	[Superoffice7].[CRM7].[Phone]	AS DirectPhone
			ON		DirectPhone.owner_id = Person.person_id
			AND		DirectPhone.ptype_idx = 16385 --Direct Phone
			AND		DirectPhone.Rank = 1
			
		LEFT JOIN	[Superoffice7].[CRM7].[Phone]	AS MobilePhone
			ON		MobilePhone.owner_id = Person.person_id
			AND		MobilePhone.ptype_idx = 16389 --Mobile Phone
			AND		MobilePhone.Rank = 1

		WHERE		[Person].[Retired] = 0
		
	MERGE [CrmReplicationEmail] AS [TargetDatabase]
	USING @Email AS [SourceDatabase]
	ON [TargetDatabase].[Id] = [SourceDatabase].[Id]	
	WHEN NOT MATCHED BY SOURCE THEN DELETE	
	WHEN MATCHED THEN 
		UPDATE SET	[TargetDatabase].[ContactId] =			[SourceDatabase].[ContactId],
					[TargetDatabase].[PersonId] =			[SourceDatabase].[PersonId],
					[TargetDatabase].[ContactName] =		[SourceDatabase].[ContactName],
					[TargetDatabase].[FirstName] =			[SourceDatabase].[FirstName],
					[TargetDatabase].[LastName]	=			[SourceDatabase].[LastName],
					[TargetDatabase].[Email] =				[SourceDatabase].[Email],
					[TargetDatabase].[DirectPhone]	=		[SourceDatabase].[DirectPhone],
					[TargetDatabase].[MobilePhone] =		[SourceDatabase].[MobilePhone]
	WHEN NOT MATCHED THEN
		INSERT ([Id],
				[ContactId],
				[PersonId],
				[ContactName],
				[FirstName],
				[LastName],
				[Email],
				[DirectPhone],
				[MobilePhone]
		)
		VALUES ([SourceDatabase].[Id],
				[SourceDatabase].[ContactId],
				[SourceDatabase].[PersonId],
				[SourceDatabase].[ContactName],
				[SourceDatabase].[FirstName],
				[SourceDatabase].[LastName],
				[SourceDatabase].[Email],
				[SourceDatabase].[DirectPhone],
				[SourceDatabase].[MobilePhone]);

	EXEC [dbo].[CrmReplicationJob_StopProcess] @JobHistoryId
END