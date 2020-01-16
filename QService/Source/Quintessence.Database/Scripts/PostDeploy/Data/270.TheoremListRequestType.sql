IF NOT EXISTS(SELECT * FROM [TheoremListRequestType] WHERE Id = 1)
BEGIN
	INSERT INTO [TheoremListRequestType](Id, Type) VALUES(1, 'As is')
END

IF NOT EXISTS(SELECT * FROM [TheoremListRequestType] WHERE Id = 2)
BEGIN
	INSERT INTO [TheoremListRequestType](Id, Type) VALUES(2, 'As is & to be')
END