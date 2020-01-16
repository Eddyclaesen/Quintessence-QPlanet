IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 100)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (100, 'Draft', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 110)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (110, 'To verify', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 150)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (150, 'Contested by proma', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 200)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (200, 'Ready for approval', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 210)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (210, 'Sent for approval', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 220)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (220, 'Ready for invoicing', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 250)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (250, 'Contested by customer', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 300)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (300, 'Ready for import', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 400)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (400, 'Imported', NULL)

IF NOT EXISTS(SELECT * FROM [TimesheetEntryStatus] WHERE [Id] = 900)
	INSERT INTO [TimesheetEntryStatus]([Id], [Name], [Description]) VALUES (900, 'Fixed price', NULL)