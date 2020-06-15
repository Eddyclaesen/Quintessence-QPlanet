CREATE TABLE [dbo].[ActivityDetailTraining2TrainingType] (
    [ActivityDetailTrainingId] UNIQUEIDENTIFIER NOT NULL,
    [TrainingTypeId]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ActivityDetailTraining2TrainingType] PRIMARY KEY NONCLUSTERED ([ActivityDetailTrainingId] ASC, [TrainingTypeId] ASC),
    CONSTRAINT [FK_ActivityDetailTraining2TrainingType_ActivityDetailTraining] FOREIGN KEY ([ActivityDetailTrainingId]) REFERENCES [dbo].[ActivityDetailTraining] ([Id]),
    CONSTRAINT [FK_ActivityDetailTraining2TrainingType_TrainingType] FOREIGN KEY ([TrainingTypeId]) REFERENCES [dbo].[TrainingType] ([Id])
);

