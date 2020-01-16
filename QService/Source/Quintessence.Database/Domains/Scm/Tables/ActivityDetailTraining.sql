CREATE TABLE [dbo].[ActivityDetailTraining](
	[Id]						UNIQUEIDENTIFIER		NOT NULL,
	[TargetGroup]				TEXT					NULL,
	[Duration]					TEXT					NULL,
	[ExtraInfo]					TEXT					NULL,
	[ChecklistLink]				TEXT					NULL,
	[Code]						NVARCHAR(12)			NULL
)