CREATE TABLE [dbo].[ActivityType2Profile](
	[Id]						UNIQUEIDENTIFIER		NOT NULL,
	[ActivityTypeId]			UNIQUEIDENTIFIER		NOT NULL,
	[ProfileId]					UNIQUEIDENTIFIER		NOT NULL,
	[DayRate]					DECIMAL(18,2)			NOT NULL,
	[HalfDayRate]				DECIMAL(18,2)			NOT NULL,
	[HourlyRate]				DECIMAL(18,2)			NOT NULL,
	[IsolatedHourlyRate]		DECIMAL(18,2)			NOT NULL,
	[Audit_CreatedBy]			NVARCHAR(MAX)			NOT NULL	DEFAULT (suser_sname()),
	[Audit_CreatedOn]			DATETIME				NOT NULL	DEFAULT GETDATE(),
	[Audit_ModifiedBy]			NVARCHAR(MAX)			NULL,
	[Audit_ModifiedOn]			DATETIME				NULL,
	[Audit_DeletedBy]			NVARCHAR(MAX)			NULL,
	[Audit_DeletedOn]			DATETIME				NULL,
	[Audit_IsDeleted]			BIT						NOT NULL	DEFAULT 0,
	[Audit_VersionId]			UNIQUEIDENTIFIER		NOT NULL	DEFAULT NEWID()
)