DECLARE @AdminId AS UNIQUEIDENTIFIER =				'E60C52B1-942B-4735-BFD9-5C05E542C79E'
DECLARE @CustomerAssistantId AS UNIQUEIDENTIFIER =	'D3D0E788-BED4-49EF-A93E-78542D7296CE'
DECLARE @ConsultantId AS UNIQUEIDENTIFIER =			'359B8FA6-F2B5-4707-B2A2-394214074A0D'
DECLARE @AccountantId AS UNIQUEIDENTIFIER =			'F52EC0A3-F44A-4CD7-B8AA-C46A019088DD'

INSERT INTO [Role]([Id], [Code], [Name], [Description]) VALUES(@AdminId, 'ADMIN', 'Admin', 'Role for administrators. This role has all the rights to all the operations.')
INSERT INTO [Role]([Id], [Code], [Name], [Description]) VALUES(@CustomerAssistantId, 'CUSTA', 'Customer Assistant', 'Role for Customer Assistants.')
INSERT INTO [Role]([Id], [Code], [Name], [Description]) VALUES(@ConsultantId, 'CONSULT', 'Consultant', 'Role for Consultants.')
INSERT INTO [Role]([Id], [Code], [Name], [Description]) VALUES(@AccountantId, 'ACCOUNT', 'Accountant', 'Role for Accountants.')
GO