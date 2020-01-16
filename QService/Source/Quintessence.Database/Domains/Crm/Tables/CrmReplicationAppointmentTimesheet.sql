CREATE TABLE [dbo].[CrmReplicationAppointmentTimesheet](
	[Id]				INT,
	[ProjectId]			INT,
	[AssociateId]		INT,
	[StartDate]			DATETIME2,
	[EndDate]			DATETIME2,
	[Description]		NVARCHAR(MAX)
)