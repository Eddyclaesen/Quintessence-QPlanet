USE [TeamLeader]
GO

ALTER TABLE TimeTracking
ADD RelatedToObjectType NVARCHAR(64) NULL,
    RelatedToObjectId INT NULL
GO

