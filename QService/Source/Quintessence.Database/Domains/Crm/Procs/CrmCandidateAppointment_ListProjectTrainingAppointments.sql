CREATE PROCEDURE [dbo].[CrmCandidateAppointment_ListProjectTrainingAppointments]
	@QProjectId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[CrmReplicationAppointmentTraining].*

	FROM		[Project2CrmProjectView]

	INNER JOIN	[CrmReplicationAppointmentTraining]	WITH (NOLOCK)
		ON		[CrmReplicationAppointmentTraining].[ProjectId] = [Project2CrmProjectView].[CrmProjectId]

	WHERE		[Project2CrmProjectView].[ProjectId] = @QProjectId
END
GO