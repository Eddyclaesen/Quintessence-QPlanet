CREATE PROCEDURE [dbo].[CrmReplicationJob_StartProcess]
	@JobName		NVARCHAR(MAX),
	@JobHistoryId	UNIQUEIDENTIFIER	OUTPUT
AS
BEGIN
	DECLARE	@JobId AS UNIQUEIDENTIFIER

	SELECT	@JobId = [Id] FROM [CrmReplicationJob] WHERE	[Name] = @JobName

	IF (@JobId IS NULL)
	BEGIN
		SELECT @JobId = NEWID()

		INSERT INTO [CrmReplicationJob]([Id], [Name]) VALUES(@JobId, @JobName)
	END

	SELECT @JobHistoryId = NEWID()

	INSERT INTO [CrmReplicationJobHistory]([Id], [CrmReplicationJobId], [StartDate]) VALUES (@JobHistoryId, @JobId, GETDATE())
END