CREATE PROCEDURE Simulation_CreateCombination
	@SimulationSet				NVARCHAR(MAX),
	@SimulationDepartment		NVARCHAR(MAX),
	@SimulationLevel			NVARCHAR(MAX),
	@Simulation					NVARCHAR(MAX),
	@Preparation				NVARCHAR(MAX),
	@Execution					NVARCHAR(MAX),
	@Languages					NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SimulationSetId AS UNIQUEIDENTIFIER
	DECLARE @SimulationDepartmentId AS UNIQUEIDENTIFIER
	DECLARE @SimulationLevelId AS UNIQUEIDENTIFIER
	DECLARE @SimulationId AS UNIQUEIDENTIFIER
	DECLARE	@SimulationCombinationId AS UNIQUEIDENTIFIER
	DECLARE @PreparationTime AS INT
	DECLARE @ExecutionTime AS INT

	DECLARE @SimulationSetLegacyId AS INT
	DECLARE @SimulationDepartmentLegacyId AS INT
	DECLARE @SimulationLevelLegacyId AS INT
	DECLARE @SimulationLegacyId AS INT

	--SimulationSet
	IF NOT EXISTS (SELECT TOP 1 1 FROM [SimulationSet] WHERE [SimulationSet].[Name] = @SimulationSet)
	BEGIN		
		INSERT INTO [SimulationSet]([Id], [Name]) VALUES (NEWID(), @SimulationSet)		
	END
	SELECT	@SimulationSetId = [Id] FROM [SimulationSet] WHERE [SimulationSet].[Name] = @SimulationSet

	--SimulationDepartment
	IF @SimulationDepartment IS NOT NULL AND @SimulationDepartment <> ''
	BEGIN
		IF NOT EXISTS (SELECT TOP 1 1 FROM [SimulationDepartment] WHERE [SimulationDepartment].[Name] = @SimulationDepartment)
		BEGIN
			INSERT INTO [SimulationDepartment]([Id], [Name]) VALUES (NEWID(), @SimulationDepartment)
		END
		SELECT	@SimulationDepartmentId = [Id] FROM [SimulationDepartment] WHERE [SimulationDepartment].[Name] = @SimulationDepartment
	END

	--SimulationLevel
	IF @SimulationLevel IS NOT NULL AND @SimulationLevel <> ''
	BEGIN
			IF NOT EXISTS (SELECT TOP 1 1 FROM [SimulationLevel] WHERE [SimulationLevel].[Name] = @SimulationLevel)
			BEGIN
				INSERT INTO [SimulationLevel]([Id], [Name]) VALUES (NEWID(), @SimulationLevel)
			END
			SELECT	@SimulationLevelId = [Id] FROM [SimulationLevel] WHERE [SimulationLevel].[Name] = @SimulationLevel
	END

	--Simulation
	IF NOT EXISTS (SELECT TOP 1 1 FROM [Simulation] WHERE [Simulation].[Name] = @Simulation)
	BEGIN
		INSERT INTO [Simulation]([Id], [Name]) VALUES (NEWID(), @Simulation)
	END
	SELECT	@SimulationId = [Id] FROM [Simulation] WHERE [Simulation].[Name] = @Simulation

	SELECT	@PreparationTime = CAST(@Preparation AS INT) WHERE ISNUMERIC(@Preparation) = 1
	SELECT	@ExecutionTime = CAST(@Execution AS INT) WHERE ISNUMERIC(@Execution) = 1

	--Simulation combination
	IF NOT EXISTS (SELECT TOP 1 1 FROM [SimulationCombination]
					WHERE		[SimulationSetId] =			@SimulationSetId
					AND			[SimulationDepartmentId] =	@SimulationDepartmentId
					AND			[SimulationLevelId] =		@SimulationLevelId
					AND			[SimulationId] =			@SimulationId
					AND			[Preparation] =				@Preparation
					AND			[Execution] =				@Execution)
	BEGIN
		SELECT	@SimulationCombinationId = NEWID()
		INSERT INTO [SimulationCombination]	([Id],		[SimulationSetId],	[SimulationDepartmentId],	[SimulationLevelId],	[SimulationId], [Preparation], [Execution])
									VALUES	(@SimulationCombinationId,	@SimulationSetId,	@SimulationDepartmentId,	@SimulationLevelId,		@SimulationId,	@Preparation,	@Execution)

		INSERT INTO [SimulationCombination2Language]([SimulationCombinationId], [LanguageId]) SELECT	@SimulationCombinationId, 1 WHERE @Languages LIKE '%NL%'	
		INSERT INTO [SimulationCombination2Language]([SimulationCombinationId], [LanguageId]) SELECT	@SimulationCombinationId, 2 WHERE @Languages LIKE '%FR%'
		INSERT INTO [SimulationCombination2Language]([SimulationCombinationId], [LanguageId]) SELECT	@SimulationCombinationId, 3 WHERE @Languages LIKE '%EN%'	
		INSERT INTO [SimulationCombination2Language]([SimulationCombinationId], [LanguageId]) SELECT	@SimulationCombinationId, 4 WHERE @Languages LIKE '%DU%'
	END
END
GO
