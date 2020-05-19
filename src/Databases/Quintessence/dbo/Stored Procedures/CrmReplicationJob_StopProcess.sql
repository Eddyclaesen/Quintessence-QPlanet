CREATE PROCEDURE [dbo].[CrmReplicationJob_StopProcess]
	@JobHistoryId		UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE	[CrmReplicationJobHistory]
	SET		[EndDate] = GETDATE(),
			[Succeeded] = 1
	WHERE	[Id] = @JobHistoryId
END