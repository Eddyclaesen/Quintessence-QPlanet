CREATE FUNCTION [dbo].[TheoremListRequest_CheckCulturalFitStatus]
(
	@TheoremListRequestId UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE	@TheoremListCount INT
	DECLARE	@CompletedCount INT

	--Check how many theorem lists this request has.
	SELECT	@TheoremListCount = COUNT(*) 
	FROM	[TheoremListView]
	WHERE	[TheoremListView].[TheoremListRequestId] = @TheoremListRequestId

	--Check how many theorem lists are completed for this request.
	SELECT	@CompletedCount = COUNT(*) 
	FROM	[TheoremListView]
	WHERE	[TheoremListView].[TheoremListRequestId] = @TheoremListRequestId
		AND	[TheoremListView].[IsCompleted] = 1

	IF(@CompletedCount = @TheoremListCount) 
	BEGIN
		--Completed
		RETURN 99
	END
	ELSE IF(@CompletedCount > 0 AND @CompletedCount < @TheoremListCount) 
	BEGIN
		--In progress
		RETURN 2
	END
	
	--Not completed
	RETURN 0
	
END