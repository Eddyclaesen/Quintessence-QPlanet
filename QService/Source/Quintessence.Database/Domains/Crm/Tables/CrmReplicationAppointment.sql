CREATE TABLE [dbo].[CrmReplicationAppointment](
	[Id]						INT		NOT NULL,
	[AppointmentDate]			DATETIME,
	[EndDate]					DATETIME,
	[AssociateId]				INT,
	[IsReserved]				BIT,
	[OfficeId]					INT,
	[LanguageId]				INT,
	[Gender]					VARCHAR(1),
	[Code]						VARCHAR(12),
	[FirstName]					NVARCHAR(MAX),
	[LastName]					NVARCHAR(MAX),
	[CrmProjectId]				INT,
	[TaskId]					INT,
	[Description]				TEXT
)