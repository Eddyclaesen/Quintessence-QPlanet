IF NOT EXISTS(SELECT * FROM [TheoremListType] WHERE Id = 1)
BEGIN
	INSERT INTO [TheoremListType](Id, Type) VALUES(1, 'As is')
END

IF NOT EXISTS(SELECT * FROM [TheoremListType] WHERE Id = 2)
BEGIN
	INSERT INTO [TheoremListType](Id, Type) VALUES(2, 'To be')
END