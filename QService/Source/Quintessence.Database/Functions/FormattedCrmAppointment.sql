CREATE FUNCTION [dbo].[FormattedCrmAppointment]
(
	@QProjectId UNIQUEIDENTIFIER
)
RETURNS @Temp TABLE (
	Id		INT,
	AppointmentDate		DATETIME,
	AssociateId			INT,
	UserId				UNIQUEIDENTIFIER,
	CrmProjectId		INT,
	OfficeId			INT,
	LanguageId			INT,
	FirstName			NVARCHAR(255),
	LastName			NVARCHAR(255),
	Gender				CHAR(1),
	Code				NVARCHAR(12),
	AssessorType		NVARCHAR(2)
	)
AS
BEGIN
	/*******************************
	DECLARATION OF VARIABLES
	********************************/
	DECLARE @Delimiter		VARCHAR(5)
	DECLARE @List			VARCHAR(MAX)
	DECLARE @RowCount		INT
	DECLARE @TableOfValues	TABLE (
								RowId	SMALLINT,
								[Value] VARCHAR(MAX)
		)
	DECLARE @AppointmentId	INT
	DECLARE @Date			DATETIME
	DECLARE @AssociateId	INT
	DECLARE @UserId			UNIQUEIDENTIFIER
	DECLARE @ProjectId		INT
	DECLARE @Text			NVARCHAR(MAX)
	DECLARE @AssessorType	NVARCHAR(2)

	/*******************************
	INITIALIZATION OF VARIABLES
	********************************/
	SET @Delimiter = ';'

	/*******************************
	DECLARE CURSOR
	********************************/
	DECLARE MyCursor CURSOR
	FOR
	SELECT	a.appointment_id	AS Id,
			a.do_by		AS DATE,
			a.associate_id		AS AssociateId,
			u.Id				AS UserId,
			a.project_id		AS CrmProjectId,
			t.[text],
			CASE 
				WHEN a.task_idx = 190 THEN 'CA'
				ELSE 'LA'
			END					AS AssessorType
	FROM		[$(SUPEROFFICE7SERVER)].[$(Superoffice7)].dbo.APPOINTMENT		AS a
	INNER JOIN	[$(SUPEROFFICE7SERVER)].[$(Superoffice7)].dbo.TEXT		AS t
		ON		a.text_id = t.text_id
	INNER JOIN	[$(SUPEROFFICE7SERVER)].[$(Superoffice7)].dbo.PROJECT	AS p
		ON		a.project_id = p.project_id
	INNER JOIN	dbo.Project2CrmProjectView								AS p2p
		ON		p.project_id = p2p.CrmProjectId
	LEFT JOIN	dbo.[UserView]											AS u
		ON		u.AssociateId = a.associate_id
	WHERE (
				a.task_idx IN (
					SELECT Id
					FROM dbo.Task_RetrieveAssessmentCenterTasks()
				)
			)
		AND p2p.ProjectId = @QProjectId

	/*******************************
	START CURSOR
	********************************/
	OPEN MyCursor

	FETCH NEXT
	FROM MyCursor
	INTO @AppointmentId
		,@Date
		,@AssociateId
		,@UserId
		,@ProjectId
		,@Text
		,@AssessorType

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Text = RTRIM(SUBSTRING(@Text, 0, CHARINDEX(CHAR(13)+CHAR(10), @Text + CHAR(13)+CHAR(10), 0)))
		DECLARE @Lenstring INT
		DECLARE @WordCount SMALLINT = 1

		--RESET TABLE THAT CONTAINS SPLITTED WORDS
		DELETE FROM @TableOfValues
		
		/****SPLIT TEXT BY DELIMITER****/
		WHILE LEN(@Text) > 0
		BEGIN
			SELECT @Lenstring = (
					CASE CHARINDEX(@Delimiter, @Text)
						WHEN 0
							THEN LEN(@Text)
						ELSE (CHARINDEX(@Delimiter, @Text) - 1)
						END
					)

			INSERT INTO @TableOfValues
			SELECT @WordCount, SUBSTRING(@Text, 1, @Lenstring)

			SELECT @Text = (
					CASE (LEN(@Text) - @Lenstring)
						WHEN 0
							THEN ''
						ELSE RIGHT(@Text, LEN(@Text) - @Lenstring - 1)
						END
					)
			SET @WordCount = @WordCount + 1
		END

		/****GET NUMBER OF WORDS RETRIEVED FROM SPLIT****/
		SELECT @RowCount = Count(*)
		FROM @TableOfValues

		/****DECLARE VARIABLES FOR WORDS FROM SPLITTED TEXT****/
		DECLARE @Office			INT
		DECLARE @Language		INT
		DECLARE @FirstName		NVARCHAR(255)
		DECLARE @LastName		NVARCHAR(255)
		DECLARE @Gender			CHAR(1)
		DECLARE @Code			NVARCHAR(12)

		IF @RowCount = 6 --ALL INFORMATION IS PRESENT IN TEXT
		BEGIN
			--Retrieve Office
			SELECT @Office = ISNULL(dbo.Office_RetrieveOfficeId(RTRIM(LTRIM(Value))), 1)
			FROM @TableOfValues
			WHERE RowId = 1

			--Retrieve Language
			SELECT @Language = ISNULL(dbo.Language_RetrieveLanguageId(LTRIM(RTRIM(Value))), 1)
			FROM @TableOfValues
			WHERE RowId = 2

			--Retrieve FirstName
			SELECT @FirstName = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 3

			--Retrieve LastName
			SELECT @LastName = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 4

			--Retrieve Gender
			SELECT @Gender = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 5

			--Retrieve Code
			SELECT @Code = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 6

			--Insert retrieved values into temporary table
			INSERT INTO @Temp
			VALUES (
				@AppointmentId
				,@Date
				,@AssociateId
				,@UserId
				,@ProjectId
				,@Office
				,@Language
				,ISNULL(@FirstName, '')
				,ISNULL(@LastName, '')
				,ISNULL(@Gender, 'M')
				,ISNULL(@Code, '')
				,@AssessorType
				)

		END

		IF @RowCount = 5 --EITHER OFFICE OR LANGUAGE IS MISSING; TAKE DEFAULT FOR THE ONE MISSING
		BEGIN
			--Retrieve office if present, else take default (1 = QA)
			SELECT @Office = ISNULL(dbo.Office_RetrieveOfficeId(RTRIM(LTRIM(Value))), 1)
			FROM @TableOfValues
			WHERE RowId = 1

			--Retrieve language if present, else take default (1 = NL)
			SELECT @Language = ISNULL(dbo.Language_RetrieveLanguageId(LTRIM(RTRIM(Value))), 1)
			FROM @TableOfValues
			WHERE RowId = 1

			--Retrieve FirstName
			SELECT @FirstName = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 2

			--Retrieve LastName
			SELECT @LastName = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 3

			--Retrieve Gender
			SELECT @Gender = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 4

			--Retrieve Code
			SELECT @Code = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 5

			--Insert retrieved values into temporary table
			INSERT INTO @Temp
			VALUES (
				@AppointmentId
				,@Date
				,@AssociateId
				,@UserId
				,@ProjectId
				,@Office
				,@Language
				,ISNULL(@FirstName, '')
				,ISNULL(@LastName, '')
				,ISNULL(@Gender, 'M')
				,ISNULL(@Code, '')
				,@AssessorType
				)
		END

		IF @RowCount = 4 --NO OFFICE OR LANGUAGE, TAKE DEFAULTS FOR THEM
		BEGIN
			--Retrieve FirstName
			SELECT @FirstName = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 1

			--Retrieve LastName
			SELECT @LastName = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 2

			--Retrieve Gender
			SELECT @Gender = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 3

			--Retrieve Code
			SELECT @Code = LTRIM(RTRIM(Value))
			FROM @TableOfValues
			WHERE RowId = 4

			--Insert retrieved values into temporary table,
			--take default 1 for Office and Language
			INSERT INTO @Temp
			VALUES (
				@AppointmentId
				,@Date
				,@AssociateId
				,@UserId
				,@ProjectId
				,1
				,1
				,ISNULL(@FirstName, '')
				,ISNULL(@LastName, '')
				,ISNULL(@Gender, 'M')
				,ISNULL(@Code, '')
				,@AssessorType
				)
		END

		FETCH NEXT
		FROM MyCursor
		INTO @AppointmentId
			,@Date
			,@AssociateId
			,@UserId
			,@ProjectId
			,@Text
			,@AssessorType
	END

	CLOSE MyCursor

	RETURN
END