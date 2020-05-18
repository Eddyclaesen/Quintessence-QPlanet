CREATE TABLE [dbo].[TheoremList] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [TheoremListRequestId] UNIQUEIDENTIFIER NOT NULL,
    [IsCompleted]          BIT              DEFAULT ((0)) NOT NULL,
    [TheoremListTypeId]    INT              NOT NULL,
    [VerificationCode]     VARCHAR (6)      NOT NULL,
    [Audit_CreatedBy]      NVARCHAR (MAX)   DEFAULT (suser_sname()) NOT NULL,
    [Audit_CreatedOn]      DATETIME         DEFAULT (getdate()) NOT NULL,
    [Audit_ModifiedBy]     NVARCHAR (MAX)   NULL,
    [Audit_ModifiedOn]     DATETIME         NULL,
    [Audit_DeletedBy]      NVARCHAR (MAX)   NULL,
    [Audit_DeletedOn]      DATETIME         NULL,
    [Audit_IsDeleted]      BIT              DEFAULT ((0)) NOT NULL,
    [Audit_VersionId]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TheoremList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TheoremList_TheoremListRequest] FOREIGN KEY ([TheoremListRequestId]) REFERENCES [dbo].[TheoremListRequest] ([Id]),
    CONSTRAINT [FK_TheoremList_TheoremListType] FOREIGN KEY ([TheoremListTypeId]) REFERENCES [dbo].[TheoremListType] ([Id])
);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[trgTheoremList]
   ON  [dbo].[TheoremList]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Mail nvarchar(max)

    IF (UPDATE([IsCompleted]) AND (SELECT [IsCompleted] from inserted) = 1)
		BEGIN
			IF (SELECT CandidateId from TheoremListRequestView where Id = (select i.TheoremListRequestId from inserted i)) is not null
				BEGIN			
					SET @Mail = (SELECT mail.Email
						FROM Quintessence.dbo.ProjectCandidateView
						LEFT JOIN Quintessence.dbo.CrmReplicationAppointment on ProjectCandidateView.CrmCandidateAppointmentId = CrmReplicationAppointment.Id
						LEFT JOIN Quintessence.dbo.CrmReplicationEmailAssociate mail on CrmReplicationAppointment.AssociateId = mail.AssociateId
						WHERE ProjectCandidateView.ProjectId = (SELECT ProjectId from TheoremListRequestView where Id = (select i.TheoremListRequestId from inserted i))
						AND ProjectCandidateView.CandidateId = (SELECT CandidateId from TheoremListRequestView where Id = (select i.TheoremListRequestId from inserted i)))
					IF @Mail is not null INSERT INTO DataWarehouse.dbo.MailQueue VALUES ('CulturalFit',0,GETDATE(),NULL,ISNULL(@Mail,'eddy@quintessence.be'),0,(select i.TheoremListRequestId from inserted i))
				END
		END
END
