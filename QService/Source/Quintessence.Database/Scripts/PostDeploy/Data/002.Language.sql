IF NOT EXISTS(SELECT * FROM Language WHERE Name = 'Nederlands')
	INSERT INTO Language(Name, Code) VALUES ('Nederlands', 'NL')

IF NOT EXISTS(SELECT * FROM Language WHERE Name = 'Français')
	INSERT INTO Language(Name, Code) VALUES ('Français', 'FR')

IF NOT EXISTS(SELECT * FROM Language WHERE Name = 'English')
	INSERT INTO Language(Name, Code) VALUES ('English', 'EN')

IF NOT EXISTS(SELECT * FROM Language WHERE Name = 'Deutsch')
	INSERT INTO Language(Name, Code) VALUES ('Deutsch', 'DE')