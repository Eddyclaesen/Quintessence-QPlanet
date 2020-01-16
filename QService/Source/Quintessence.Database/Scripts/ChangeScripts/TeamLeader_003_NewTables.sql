
-- CrmReplicationSettings
CREATE TABLE [dbo].[CrmReplicationSettings]
(
	[Id] [int] NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL
    CONSTRAINT [PK_CrmReplicationSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- CrmReplicationProjectStatusMapping
CREATE TABLE [dbo].[CrmReplicationProjectStatusMapping]
(
	[Id] [int] NOT NULL,
	[SourceValue] [nvarchar](max) NULL,
	[TargetId] INT NOT NULL
    CONSTRAINT [PK_CrmReplicationProjectStatusMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

----------------------------------------------------------------------------------------------------------------------------------------
-- History tables :

CREATE TABLE [dbo].[CrmReplicationProjectHistory]
(
	[CrmReplicationProjectHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [INT] NOT NULL,
	[Name] [NVARCHAR](MAX) NULL,
	[AssociateId] [INT] NULL,
	[ContactId] [INT] NULL,
	[ProjectStatusId] [INT] NULL,
	[StartDate] [DATETIME] NULL,
	[BookyearFrom] [DATETIME] NULL,
	[BookyearTo] [DATETIME] NULL,
	[TeamLeaderId] [INT] NULL,
	[LastSyncedUtc] [DATETIME] NULL,
	[SyncVersion] [INT] NOT NULL
 CONSTRAINT [PK_CrmReplicationProjectHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationProjectHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CrmReplicationPersonHistory]
(
	[CrmReplicationPersonHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[ContactId] [int] NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[IsRetired] [bit] NULL,
	[TeamLeaderId] [int] NULL,
	[LastSyncedUtc] [DATETIME] NULL,
	[SyncVersion] [INT] NOT NULL
 CONSTRAINT [PK_CrmReplicationPersonHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationPersonHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO 

CREATE TABLE [dbo].[CrmReplicationEmailHistory]
(
	[CrmReplicationEmailHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[PersonId] [int] NULL,
	[ContactId] [int] NULL,
	[ContactName] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[DirectPhone] [nvarchar](max) NULL,
	[MobilePhone] [nvarchar](max) NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL
 CONSTRAINT [PK_CrmReplicationEmailHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationEmailHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CrmReplicationContactHistory]
(
	[CrmReplicationContactHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[AssociateId] [int] NULL,
	[AccountManagerId] [int] NULL,
	[CustomerAssistantId] [int] NULL,
	[TeamLeaderId] [int] NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationContactHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationContactHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CrmReplicationAssociateHistory]
(
	[CrmReplicationAssociateHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserGroupId] [int] NULL,
	[TeamLeaderName] [nvarchar](max) NULL,
	[TeamLeaderId] [int] NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationAssociateHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationAssociateHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CrmReplicationEmailAssociateHistory]
(
	[CrmReplicationEmailAssociateHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[AssociateId] [int] NULL,
	[Email] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL
 CONSTRAINT [PK_CrmReplicationEmailAssociateHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationEmailAssociateHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CrmReplicationAppointmentTrainingHistory]
(
	[CrmReplicationAppointmentTrainingHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[ProjectId] [int] NULL,
	[AssociateId] [int] NULL,
	[OfficeId] [int] NULL,
	[LanguageId] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Code] [nvarchar](12) NULL,
	[Description] [nvarchar](max) NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationAppointmentTrainingHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationAppointmentTrainingHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CrmReplicationAppointmentTimesheetHistory]
(
	[CrmReplicationAppointmentTimesheetHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[ProjectId] [int] NULL,
	[AssociateId] [int] NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[Description] [nvarchar](max) NULL,
	[TaskDescription] [nvarchar](150) NULL,
	[TeamLeaderId] [INT] NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationAppointmentTimesheetHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationAppointmentTimesheetHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CrmReplicationAppointmentHistory]
(
	[CrmReplicationAppointmentHistoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[AppointmentDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AssociateId] [int] NULL,
	[IsReserved] [bit] NULL,
	[OfficeId] [int] NULL,
	[LanguageId] [int] NULL,
	[Gender] [varchar](1) NULL,
	[Code] [varchar](12) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[CrmProjectId] [int] NULL,
	[TaskId] [int] NULL,
	[Description] [text] NULL,
	[TeamLeaderId] [int] NULL,
	[LastSyncedUtc] [datetime] NULL,
	[SyncVersion] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationAppointmentHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationAppointmentHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

----------------------------------------------------------------------------------------------------------------------------------------
-- WebHook tables :

CREATE TABLE [dbo].[CrmReplicationTeamLeaderEvent]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventType] [nvarchar](max) NULL,
	[ObjectType] [nvarchar](max) NULL,
	[ObjectId] [nvarchar](max) NULL,
	[Source] [nvarchar](10) NOT NULL,
	[ReceivedUtc] [datetime] NOT NULL,
	[ProcessCount] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationTeamLeaderEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[CrmReplicationTeamLeaderEventHistory]
(
	[CrmReplicationTeamLeaderEventHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[EventType] [nvarchar](max) NULL,
	[ObjectType] [nvarchar](max) NULL,
	[ObjectId] [nvarchar](max) NULL,
	[Source] [nvarchar](10) NOT NULL,
	[ReceivedUtc] [datetime] NOT NULL,
	[ProcessCount] [int] NOT NULL,
 CONSTRAINT [PK_CrmReplicationTeamLeaderEventHistory] PRIMARY KEY CLUSTERED 
(
	[CrmReplicationTeamLeaderEventHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CrmReplicationTeamLeaderEventErrorLog]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CrmReplicationTeamLeaderEventId] [int] NOT NULL,
	[LogDateUtc] [datetime] NOT NULL,
	[Info] [nvarchar](max) NULL
 CONSTRAINT [PK_CrmReplicationTeamLeaderEventErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

----------------------------------------------------------------------------------------------------------------------------------------
