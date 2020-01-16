CREATE VIEW [dbo].[CrmAppointmentView]
	AS 
	SELECT		*

	FROM		[CrmReplicationAppointment]	WITH (NOLOCK)

