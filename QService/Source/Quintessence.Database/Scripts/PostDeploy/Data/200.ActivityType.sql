DECLARE @ConsultingActivityType AS UNIQUEIDENTIFIER = NEWID()
DECLARE @SupportActivityType AS UNIQUEIDENTIFIER = NEWID()
DECLARE @WorkshopActivityType AS UNIQUEIDENTIFIER = NEWID()
DECLARE @CoachingActivityType AS UNIQUEIDENTIFIER = NEWID()
DECLARE @TrainigActivityType AS UNIQUEIDENTIFIER = NEWID()
DECLARE @TravelActivityType AS UNIQUEIDENTIFIER = NEWID()

DECLARE @LouProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @ManagementConsultantProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @SeniorConsultantProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @JuniorConsultantProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @ItProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @AdministrationProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @MarketingProfile AS UNIQUEIDENTIFIER = NEWID()
DECLARE @StandardProfile AS UNIQUEIDENTIFIER = NEWID()

INSERT INTO [ActivityType] (Id, Name, IsSystem) VALUES (@ConsultingActivityType, 'Consulting', 1)
INSERT INTO [ActivityType] (Id, Name, IsSystem) VALUES (@SupportActivityType, 'Support', 1)
INSERT INTO [ActivityType] (Id, Name, IsSystem) VALUES (@WorkshopActivityType, 'Workshop', 1)
INSERT INTO [ActivityType] (Id, Name, IsSystem) VALUES (@CoachingActivityType, 'Coaching', 1)
INSERT INTO [ActivityType] (Id, Name, IsSystem) VALUES (@TrainigActivityType, 'Training', 1)
INSERT INTO [ActivityType] (Id, Name, IsSystem) VALUES (@TravelActivityType, 'Travel', 1)

INSERT INTO [Profile] (Id, Name) VALUES (@LouProfile, 'Lou')
INSERT INTO [Profile] (Id, Name) VALUES (@ManagementConsultantProfile, 'Management Consultant')
INSERT INTO [Profile] (Id, Name) VALUES (@SeniorConsultantProfile, 'Senior Consultant')
INSERT INTO [Profile] (Id, Name) VALUES (@JuniorConsultantProfile, 'Junior Consultant')
INSERT INTO [Profile] (Id, Name) VALUES (@ItProfile, 'IT')
INSERT INTO [Profile] (Id, Name) VALUES (@AdministrationProfile, 'Administration')
INSERT INTO [Profile] (Id, Name) VALUES (@MarketingProfile, 'Marketing')
INSERT INTO [Profile] (Id, Name) VALUES (@StandardProfile, 'Standard')

--Consulting
INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @ConsultingActivityType, @LouProfile, 2306, 1153, 288, 343)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @ConsultingActivityType, @ManagementConsultantProfile, 1973, 987, 247, 287)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @ConsultingActivityType, @SeniorConsultantProfile, 1758, 879, 220, 267)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @ConsultingActivityType, @JuniorConsultantProfile, 1599, 800, 200, 241)

--Support
INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @SupportActivityType, @ItProfile, 820, 410, 103, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @SupportActivityType, @AdministrationProfile, 513, 256, 64, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @SupportActivityType, @MarketingProfile, 615, 308, 77, 0)

--Workshop
INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @WorkshopActivityType, @LouProfile, 2306, 1384, 0, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @WorkshopActivityType, @ManagementConsultantProfile, 1973, 1184, 0, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @WorkshopActivityType, @SeniorConsultantProfile, 1896, 1138, 0, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @WorkshopActivityType, @JuniorConsultantProfile, 1896, 1138, 0, 0)

--Coaching
INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @CoachingActivityType, @StandardProfile, 1896, 1138, 266.5, 0)

--Training
INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TrainigActivityType, @LouProfile, 2306, 1384, 0, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TrainigActivityType, @ManagementConsultantProfile, 1973, 1184, 0, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TrainigActivityType, @SeniorConsultantProfile, 1896, 1138, 0, 0)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TrainigActivityType, @JuniorConsultantProfile, 1896, 1138, 0, 0)

--Consulting
INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TravelActivityType, @LouProfile, 0, 0, 288, 343)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TravelActivityType, @ManagementConsultantProfile, 0, 0, 247, 287)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TravelActivityType, @SeniorConsultantProfile, 0, 0, 220, 267)

INSERT INTO [ActivityType2Profile] (Id, ActivityTypeId, ProfileId, DayRate, HalfDayRate, HourlyRate, IsolatedHourlyRate)
VALUES(NEWID(), @TravelActivityType, @JuniorConsultantProfile, 0, 0, 200, 241)


--Add Training types
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'Certified competency assessor')
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'Certified personal coach')
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'Gedragsgericht interviewen')
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'Direct aan de slag met competenties')
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'Medewerkers begeleiden in hun ontwikkeling')
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'POP-workshop')
INSERT INTO [TrainingType](Id, Name) VALUES(NEWID(), 'Andere')