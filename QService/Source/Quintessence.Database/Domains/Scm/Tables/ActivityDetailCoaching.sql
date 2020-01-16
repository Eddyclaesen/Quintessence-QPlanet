CREATE TABLE [dbo].[ActivityDetailCoaching](
	[Id]						UNIQUEIDENTIFIER		NOT NULL,
	[TargetGroup]				TEXT					NULL,
	[SessionQuantity]			INT						NOT NULL	DEFAULT(1)
)