BEGIN TRAN
INSERT INTO dbo.Candidate(Id, FirstName, LastName, Email, Gender, LanguageId, LegacyId)
	SELECT		NEWID(),
				CandidateInfo2Appointment.FirstName,
				CandidateInfo2Appointment.LastName,
				NULL,
				CASE CandidateInfo2Appointment.Gender
					WHEN 1 THEN 'M'
					WHEN 2 THEN 'F'
					ELSE 'M'
				END,
				CandidateInfo2Appointment.LanguageId,
				CandidateInfo2Appointment.CandidateInfoId
	
	FROM		ACTSERVER.ACT.dbo.CandidateInfo2Appointment CandidateInfo2Appointment
	WHERE		CandidateInfo2Appointment.AppointmentId IS NOT NULL
ROLLBACK