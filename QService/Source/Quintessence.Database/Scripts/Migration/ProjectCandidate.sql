BEGIN TRAN	
INSERT INTO dbo.ProjectCandidate(
									Id,
									CandidateId,
									CrmCandidateAppointmentId,
									CrmCandidateInfoId,
									ProjectId,
									ReportDeadline,
									ReportLanguageId,
									ReportStatusId,
									InvoiceStatusCode)
	SELECT		NEWID(), 
				dbo.Candidate.Id, 
				CandidateInfo2Appointment.AppointmentId, 
				SoCandidate.Kand_Inf_ID,
				dbo.Project.Id,
				DATEADD(DAY, 2, Appointment.do_by),
				ISNULL(SoCandidate.VerslagTaal_ID, 1),
				1,
				100
				
	FROM		ACTSERVER.ACT.dbo.CandidateInfo2Appointment CandidateInfo2Appointment
	
	INNER JOIN	SUPEROFFICE7SERVER.Superoffice7.dbo.Kand_Inf SoCandidate 
		ON		SoCandidate.Kand_Inf_ID = CandidateInfo2Appointment.CandidateInfoId
		
	INNER JOIN	SUPEROFFICE7SERVER.Superoffice7.dbo.APPOINTMENT Appointment
		ON		Appointment.appointment_id = CandidateInfo2Appointment.AppointmentId
		
	INNER JOIN	ACTSERVER.ACT.dbo.ProjectFiche ProjectFiche 
		ON		ProjectFiche.ACProject_ID = SoCandidate.ACProject_ID
		
	INNER JOIN	dbo.Project 
		ON		dbo.Project.LegacyId = ProjectFiche.Project_ID
		
	INNER JOIN	dbo.Candidate 
		ON		dbo.Candidate.legacyId = SoCandidate.Kand_Inf_ID

COMMIT	