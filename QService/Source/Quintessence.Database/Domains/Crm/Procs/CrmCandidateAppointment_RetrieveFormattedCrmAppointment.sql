CREATE PROCEDURE CrmCandidateAppointment_RetrieveFormattedCrmAppointment	
	@AppointmentId		INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT		[CrmAppointmentView].*
	
	FROM		[CrmAppointmentView]

	WHERE		[CrmAppointmentView].[Id] = @AppointmentId
END
GO