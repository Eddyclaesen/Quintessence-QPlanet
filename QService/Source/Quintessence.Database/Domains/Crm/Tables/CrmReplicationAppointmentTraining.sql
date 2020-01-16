CREATE TABLE [dbo].[CrmReplicationAppointmentTraining](
	[Id]					INT,
	[ProjectId]				INT,
	[AssociateId]			INT,
	[OfficeId]				INT,
	[LanguageId]			INT,
	[StartDate]				DATETIME,
	[EndDate]				DATETIME,
	[Code]					NVARCHAR(12),
	[Description]			NVARCHAR(MAX)
)