CREATE PROCEDURE [dbo].[QCare_ListEvaluationFormAnswers]
	@EvaluationFormId		UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @QuestionAnswerTable AS TABLE (
		Question NVARCHAR(MAX)
		,Answer NVARCHAR(MAX)
		)
	DECLARE @ColumnName NVARCHAR(MAX)
	DECLARE @ColumnValue NVARCHAR(MAX)
	DECLARE @Sql NVARCHAR(MAX)
	DECLARE @ParamDefinition NVARCHAR(MAX)

	--ACDC Form
	IF (
			EXISTS (
				SELECT *
				FROM [dbo].[EvaluationFormAcdcView]
				WHERE [Id] = @EvaluationFormId
				)
			)
	BEGIN
		DECLARE AcdcFormCursor CURSOR
		FOR
		SELECT COLUMN_NAME AS ColumnName
		FROM INFORMATION_SCHEMA.COLUMNS
		WHERE TABLE_NAME = 'EvaluationFormAcdcView'
		AND SUBSTRING(COLUMN_NAME,1,8) = 'Question'

		OPEN AcdcFormCursor

		FETCH NEXT
		FROM AcdcFormCursor
		INTO @ColumnName

		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			SET @ColumnValue = ''
			SET @Sql = 'SELECT @RetrievedColumnValue = {0} FROM EvaluationFormAcdcView WHERE Id = ''{1}'''
			SET @Sql = REPLACE(@Sql, '{0}', @ColumnName)
			SET @Sql = REPLACE(@Sql, '{1}', @EvaluationFormId)
			SET @ParamDefinition = N'@RetrievedColumnValue NVARCHAR(MAX) OUTPUT';

			EXECUTE sp_executesql @Sql
				,@ParamDefinition
				,@RetrievedColumnValue = @ColumnValue OUTPUT;

			INSERT INTO @QuestionAnswerTable
			VALUES (
				@ColumnName
				,@ColumnValue
				)

			FETCH NEXT
			FROM AcdcFormCursor
			INTO @ColumnName
		END
		
		SELECT * FROM @QuestionAnswerTable
	END

	--Coaching Form
	IF (
			EXISTS (
				SELECT *
				FROM [dbo].[EvaluationFormCoachingView]
				WHERE [Id] = @EvaluationFormId
				)
			)
	BEGIN
		DECLARE CoachingFormCursor CURSOR
		FOR
		SELECT COLUMN_NAME AS ColumnName
		FROM INFORMATION_SCHEMA.COLUMNS
		WHERE TABLE_NAME = 'EvaluationFormCoachingView'
		AND SUBSTRING(COLUMN_NAME,1,8) = 'Question'

		OPEN CoachingFormCursor

		FETCH NEXT
		FROM CoachingFormCursor
		INTO @ColumnName

		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			SET @ColumnValue = ''
			SET @Sql = 'SELECT @RetrievedColumnValue = {0} FROM EvaluationFormCoachingView WHERE Id = ''{1}'''
			SET @Sql = REPLACE(@Sql, '{0}', @ColumnName)
			SET @Sql = REPLACE(@Sql, '{1}', @EvaluationFormId)
			SET @ParamDefinition = N'@RetrievedColumnValue NVARCHAR(MAX) OUTPUT';

			EXECUTE sp_executesql @Sql
				,@ParamDefinition
				,@RetrievedColumnValue = @ColumnValue OUTPUT;

			INSERT INTO @QuestionAnswerTable
			VALUES (
				@ColumnName
				,@ColumnValue
				)

			FETCH NEXT
			FROM CoachingFormCursor
			INTO @ColumnName
		END
		
		SELECT * FROM @QuestionAnswerTable
	END

	--Custom Projects Form
	IF (
			EXISTS (
				SELECT *
				FROM [dbo].[EvaluationFormCustomProjectsView]
				WHERE [Id] = @EvaluationFormId
				)
			)
	BEGIN
		DECLARE CustomProjectsFormCursor CURSOR
		FOR
		SELECT COLUMN_NAME AS ColumnName
		FROM INFORMATION_SCHEMA.COLUMNS
		WHERE TABLE_NAME = 'EvaluationFormCustomProjectsView'
		AND SUBSTRING(COLUMN_NAME,1,8) = 'Question'

		OPEN CustomProjectsFormCursor

		FETCH NEXT
		FROM CustomProjectsFormCursor
		INTO @ColumnName

		WHILE @@FETCH_STATUS = 0
		BEGIN

			SET @ColumnValue = ''
			SET @Sql = 'SELECT @RetrievedColumnValue = {0} FROM EvaluationFormCustomProjectsView WHERE Id = ''{1}'''
			SET @Sql = REPLACE(@Sql, '{0}', @ColumnName)
			SET @Sql = REPLACE(@Sql, '{1}', @EvaluationFormId)
			SET @ParamDefinition = N'@RetrievedColumnValue NVARCHAR(MAX) OUTPUT';

			EXECUTE sp_executesql @Sql
				,@ParamDefinition
				,@RetrievedColumnValue = @ColumnValue OUTPUT;

			INSERT INTO @QuestionAnswerTable
			VALUES (
				@ColumnName
				,@ColumnValue
				)


			FETCH NEXT
			FROM CustomProjectsFormCursor
			INTO @ColumnName
		END
		
		SELECT * FROM @QuestionAnswerTable
	END
END
GO