IF NOT EXISTS(SELECT * FROM [SurveyPlanning] WHERE Text = 'During')
	INSERT INTO [SurveyPlanning](Text) VALUES ('During')

IF NOT EXISTS(SELECT * FROM [SurveyPlanning] WHERE Text = 'Before')
	INSERT INTO [SurveyPlanning](Text) VALUES ('Before')

IF NOT EXISTS(SELECT * FROM [SurveyPlanning] WHERE Text = 'After')
	INSERT INTO [SurveyPlanning](Text) VALUES ('After')

IF NOT EXISTS(SELECT * FROM [SurveyPlanning] WHERE Text = 'N/A')
	INSERT INTO [SurveyPlanning](Text) VALUES ('N/A')