CREATE FUNCTION [dbo].[Task_RetrieveAssessmentCenterTasks]()
	RETURNS @TaskIdTable TABLE(Id INT)
AS
BEGIN
	INSERT INTO @TaskIdTable(Id)
		SELECT Task_id 
		FROM [SUPEROFFICE7SERVER].[Superoffice7].[dbo].[TASK]
		WHERE name IN (
				'AC'
				,'DC'
				,'FA'
				,'FD'
				,'SO'
				,'Exec AC'
				,'OC'
				,'PS'
				,'Co-Assessor')
	RETURN
END