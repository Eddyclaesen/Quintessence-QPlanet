IF NOT EXISTS(SELECT * FROM ReportStatus WHERE Name = 'Received')
	INSERT INTO ReportStatus(Name, Code, SortOrder) VALUES ('Received', 'RECEIVED', 10)

IF NOT EXISTS(SELECT * FROM ReportStatus WHERE Name = 'Translation agency')
	INSERT INTO ReportStatus(Name, Code, SortOrder) VALUES ('Translation agency', 'TRANSLATION', 20)

IF NOT EXISTS(SELECT * FROM ReportStatus WHERE Name = 'In progress')
	INSERT INTO ReportStatus(Name, Code, SortOrder) VALUES ('In progress', 'PROGRESS', 30)

IF NOT EXISTS(SELECT * FROM ReportStatus WHERE Name = 'Finished and read')
	INSERT INTO ReportStatus(Name, Code, SortOrder) VALUES ('Finished and read', 'FINISHEDREAD', 40)

IF NOT EXISTS(SELECT * FROM ReportStatus WHERE Name = 'Sent to customer by e-mail')
	INSERT INTO ReportStatus(Name, Code, SortOrder) VALUES ('Sent to customer by e-mail', 'SENT', 50)