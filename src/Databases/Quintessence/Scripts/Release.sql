CREATE SCHEMA [QCandidate]
    AUTHORIZATION [dbo];
GO

CREATE PROCEDURE [QCandidate].[Assessment_GetByCandidateIdAndDate]
	@candidateId UNIQUEIDENTIFIER,
	@date DATE
AS

	SET NOCOUNT ON;

SELECT
	--Customer
		cc.Id,
		cc.Name,

	--Position
		p.Id,
		p.Name,

	--DayProgram
		@date AS Date,
		--Location
			o.Id,
			o.FullName AS Name,
		--ProgramComponents
			prc.Id,
			prc.[Start],
			prc.[End],
			s.[Name],
			prc.Description,
			prc.SimulationCombinationId,
			--Room
				ar.Id,
				ar.[Name],
			--LeadAssessor
				uLeadAssess.Id,
				uLeadAssess.FirstName,
				uLeadAssess.LastName,
			--CoAssessor
				uCoAssess.Id,
				uCoAssess.FirstName,
				uCoAssess.LastName			
FROM
	dbo.candidate c WITH (NOLOCK)
	INNER JOIN dbo.ProjectCandidate pc WITH (NOLOCK)
		ON pc.CandidateId = c.Id
	INNER JOIN dbo.Project p WITH (NOLOCK)
		ON p.Id = pc.ProjectId
	INNER JOIN dbo.CrmContact cc WITH (NOLOCK)
		ON cc.Id = p.ContactId
	INNER JOIN dbo.ProgramComponent prc WITH (NOLOCK)
		ON prc.ProjectCandidateId = pc.Id
	LEFT OUTER JOIN dbo.[User] uLeadAssess WITH (NOLOCK)
		ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT OUTER JOIN dbo.[User] uCoAssess WITH (NOLOCK)
		ON uCoAssess.Id = prc.CoAssessorUserId
	LEFT OUTER JOIN (dbo.SimulationCombination sc WITH (NOLOCK)
		INNER JOIN dbo.Simulation s WITH (NOLOCK)
			ON s.Id = sc.SimulationId)
		ON sc.Id = prc.SimulationCombinationId	
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK)
		ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK)
		ON o.Id = ar.OfficeId
WHERE
	c.Id = @candidateId
	AND CONVERT(DATE, prc.Start) = @date
	AND prc.Description NOT LIKE '%Input scoring%'
	AND CONVERT(VARCHAR, prc.Description) NOT IN('Preparation consultant','Assessor debriefing','Proma','Assessor debriefing GGI')
ORDER BY
	prc.Start,
	prc.[End]
GO

CREATE PROCEDURE [QCandidate].[ProgramComponent_GetById]
	@id UNIQUEIDENTIFIER
AS

	SET NOCOUNT ON;

SELECT
	--ProgramComponents
	prc.Id,
	prc.[Start],
	prc.[End],
	s.[Name],
	prc.Description,
	prc.SimulationCombinationId,
	--Room
		ar.Id,
		ar.[Name],
	--LeadAssessor
		uLeadAssess.Id,
		uLeadAssess.FirstName,
		uLeadAssess.LastName,
	--CoAssessor
		uCoAssess.Id,
		uCoAssess.FirstName,
		uCoAssess.LastName			
FROM
	dbo.ProgramComponent prc
	LEFT OUTER JOIN dbo.[User] uLeadAssess WITH (NOLOCK)
		ON uLeadAssess.Id = prc.LeadAssessorUserId
	LEFT OUTER JOIN dbo.[User] uCoAssess WITH (NOLOCK)
		ON uCoAssess.Id = prc.CoAssessorUserId
	LEFT OUTER JOIN (dbo.SimulationCombination sc WITH (NOLOCK)
		INNER JOIN dbo.Simulation s WITH (NOLOCK)
			ON s.Id = sc.SimulationId)
		ON sc.Id = prc.SimulationCombinationId	
	INNER JOIN dbo.AssessmentRoom ar WITH (NOLOCK)
		ON ar.Id = prc.AssessmentRoomId
	INNER JOIN dbo.Office o WITH (NOLOCK)
		ON o.Id = ar.OfficeId
WHERE
	prc.Id = @id
GO

ALTER VIEW [dbo].[CandidateView]
AS
SELECT
	c.Id,
	c.FirstName,
	c.LastName,
	c.Email,
	c.Gender,
	c.LanguageId,
	c.Audit_CreatedBy,
	c.Audit_CreatedOn,
	c.Audit_ModifiedBy,
	c.Audit_ModifiedOn,
	c.Audit_DeletedBy,
	c.Audit_DeletedOn,
	c.Audit_IsDeleted,
	c.Audit_VersionId, 
    c.LegacyId,
	c.Phone,
	lv.Name AS LanguageName,
	c.Reference,
	c.HasQCandidateAccess,
	c.QCandidateUserId
FROM
	dbo.Candidate AS c WITH (NOLOCK)
	INNER JOIN dbo.LanguageView AS lv
		ON c.LanguageId = lv.Id
WHERE
	(c.Audit_IsDeleted = 0)
GO

ALTER VIEW [dbo].[ProjectCandidateView]
AS
SELECT
    dbo.ProjectCandidate.Id,
    dbo.ProjectCandidate.CandidateId,
    dbo.ProjectCandidate.CrmCandidateAppointmentId,
    dbo.ProjectCandidate.CrmCandidateInfoId,
    dbo.ProjectCandidate.ProjectId, 
    dbo.ProjectCandidate.ReportDeadline,
    dbo.ProjectCandidate.ReportLanguageId,
    dbo.ProjectCandidate.ReportReviewerId,
    dbo.ProjectCandidate.ReportStatusId,
    dbo.ProjectCandidate.IsCancelled, 
    dbo.ProjectCandidate.CancelledDate,
    dbo.ProjectCandidate.CancelledAppointmentDate,
    dbo.ProjectCandidate.CancelledReason,
    dbo.ProjectCandidate.InvoiceAmount,
    dbo.ProjectCandidate.InvoiceStatusCode, 
    dbo.ProjectCandidate.InvoicedDate,
    dbo.ProjectCandidate.InvoiceRemarks,
    dbo.ProjectCandidate.PurchaseOrderNumber,
    dbo.ProjectCandidate.InvoiceNumber,
    dbo.ProjectCandidate.Remarks, 
    dbo.ProjectCandidate.ScoringCoAssessorId,
    dbo.ProjectCandidate.IsAccompaniedByCustomer,
    dbo.ProjectCandidate.FollowUpDone,
    dbo.ProjectCandidate.OrderConfirmationSentDate, 
    dbo.ProjectCandidate.OrderConfirmationReceivedDate,
    dbo.ProjectCandidate.InvitationSentDate,
    dbo.ProjectCandidate.LeafletSentDate,
    dbo.ProjectCandidate.ReportMailSentDate, 
    dbo.ProjectCandidate.DossierReadyDate,
    dbo.ProjectCandidate.ReportDeadlineDone,
    dbo.ProjectCandidate.OrderConfirmationSentDateDone,
    dbo.ProjectCandidate.OrderConfirmationReceivedDateDone, 
    dbo.ProjectCandidate.InvitationSentDateDone,
    dbo.ProjectCandidate.LeafletSentDateDone,
    dbo.ProjectCandidate.ReportMailSentDateDone,
    dbo.ProjectCandidate.DossierReadyDateDone, 
    dbo.ProjectCandidate.Extra1,
    dbo.ProjectCandidate.Extra2,
    dbo.ProjectCandidate.Extra1Done,
    dbo.ProjectCandidate.Extra2Done,
    dbo.ProjectCandidate.ProposalId,
    dbo.ProjectCandidate.Audit_CreatedBy, 
    dbo.ProjectCandidate.Audit_CreatedOn,
    dbo.ProjectCandidate.Audit_ModifiedBy,
    dbo.ProjectCandidate.Audit_ModifiedOn,
    dbo.ProjectCandidate.Audit_DeletedBy,
    dbo.ProjectCandidate.Audit_DeletedOn, 
    dbo.ProjectCandidate.Audit_IsDeleted,
    dbo.ProjectCandidate.Audit_VersionId,
    dbo.ProjectCandidate.InternalCandidate,
    dbo.CrmAppointmentView.Code,
    dbo.ProjectCandidate.Id AS ProjectCandidateDetailId, 
    COALESCE (dbo.CrmAppointmentView.OfficeId, 1) AS OfficeId,
    dbo.CandidateView.FirstName AS CandidateFirstName,
    dbo.CandidateView.LastName AS CandidateLastName, 
    dbo.CandidateView.Email AS CandidateEmail,
    dbo.CandidateView.LanguageId AS CandidateLanguageId,
    dbo.CandidateView.Gender AS CandidateGender,
    dbo.ProjectCandidate.OnlineAssessment, 
    dbo.CandidateView.Phone AS CandidatePhone,
    dbo.CandidateView.HasQCandidateAccess AS CandidateHasQCandidateAccess,
    dbo.CandidateView.QCandidateUserId AS CandidateQCandidateUserId,
    dbo.ProjectCandidate.FinancialEntityId
FROM
	dbo.ProjectCandidate WITH (NOLOCK)
	INNER JOIN dbo.CandidateView
		ON dbo.ProjectCandidate.CandidateId = dbo.CandidateView.Id
	LEFT OUTER JOIN dbo.CrmAppointmentView
		ON dbo.CrmAppointmentView.Id = dbo.ProjectCandidate.CrmCandidateAppointmentId
WHERE
	(dbo.ProjectCandidate.Audit_IsDeleted = 0)
GO

update dbo.MailTemplateTranslation set body = '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--GENDER--&gt; &lt;!--LASTNAME--&gt;,</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Op vraag van &lt;!--CONTACT--&gt; nodigen wij u<strong>&lt;!--ONLINE--&gt;</strong>&lt;!--PROJECTTYPEFOD--&gt;&lt;!--BELFIUSAC--&gt; <strong>op &lt;!--DATE--&gt;</strong></span>. <span style="color: #002649; font-family: calibri; font-size: 11pt;">Uw programma start om</span><span style="color: #002649; font-family: calibri; font-size: 11pt;">&nbsp;<strong>&lt;!--TIME--&gt;</strong> en duurt&nbsp;</span><span style="color: #002649; font-family: calibri; font-size: 14.6667px;"><strong>&lt;!--DURATION--&gt;.</strong>&nbsp;<!--FODFINPRELOC--><!--ONLINELOC--></span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--IMPORTANT--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--ONLINEDESC--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--MAIL--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--CONTEXTLOGIN--&gt;</span><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--SIMCONLOGINS--&gt;</span></strong></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--QCANDIDATELOGIN--&gt;</span></p>
<p><span style="font-size: 11pt; color: #002649; font-family: calibri;">&lt;!--SUBCATEGORIES--&gt;&lt;!--BPOST--&gt;&lt;!--BELFIUSBODY--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="font-size: 14.6667px;">&lt;!--HELP--&gt;</span></span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--CV--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--HOERABOEK--&gt;</span></p>
<!--ONLINEPARK-->
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--FODFIN--&gt;Met vriendelijke groet,</span></span><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="color: #002649; font-family: calibri; font-size: 11pt;"><br /></span></span></p>
<!--WEGENWERKEN-->
<div id="ConnectiveDocSignExtentionInstalled" data-extension-version="1.0.4">&nbsp;</div>'
where languageid = (SELECT TOP(1) Id FROM dbo.Language WHERE Code = 'NL')
	and MailTemplateId = (SELECT TOP(1) Id FROM dbo.MailTemplate WHERE Code = 'CANDINVITE')
GO

update dbo.MailTemplateTranslation set body = '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--GENDER--&gt; &lt;!--LASTNAME--&gt;,</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">Following a request from &lt;!--CONTACT--&gt;, we wish to invite you </span><strong style="color: #385623; font-family: calibri; font-size: 14.6667px;">&lt;!--ONLINE--&gt;</strong><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="color: #385623;"><strong>&lt;!--PROJECTTYPEFOD--&gt;&lt;!--BELFIUSAC--&gt; on &lt;!--DATE--&gt;</strong></span>. Your programme starts at</span><span style="color: #002649; font-family: calibri; font-size: 11pt;">&nbsp;<strong>&lt;!--TIME--&gt;</strong> and will last for&nbsp;</span><span style="color: #002649; font-family: calibri; font-size: 14.6667px;"><strong>&lt;!--DURATION--&gt;.</strong>&nbsp;<!--ONLINELOC--></span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--IMPORTANT--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--ONLINEDESC--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--MAIL--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--CONTEXTLOGIN--&gt;</span><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--SIMCONLOGINS--&gt;</span></strong></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--QCANDIDATELOGIN--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="font-size: 11pt;">&lt;!--SUBCATEGORIES--&gt;&lt;!--BPOST--&gt;&lt;!--BELFIUSBODY--&gt;</span></span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="font-size: 11pt;">&lt;!--HELP--&gt;</span></span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--CV--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--HOERABOEK--&gt;</span></p>
<!--ONLINEPARK-->
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="color: #002649; font-family: calibri; font-size: 11pt;">Kind regards,</span><br /></span></p>
<div id="ConnectiveDocSignExtentionInstalled" data-extension-version="1.0.4">&nbsp;</div>'
where languageid = (SELECT TOP(1) Id FROM dbo.Language WHERE Code = 'EN')
	and MailTemplateId = (SELECT TOP(1) Id FROM dbo.MailTemplate WHERE Code = 'CANDINVITE')
GO

update dbo.MailTemplateTranslation set body = '<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--GENDER--&gt; &lt;!--LASTNAME--&gt;,</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&Agrave; la demande du &lt;!--CONTACT--&gt;, nous vous invitons </span><strong style="color: #003873; font-family: calibri; font-size: 14.6667px;">&lt;!--ONLINE--&gt;</strong><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="color: #003873;">&lt;!--PROJECTTYPEFOD--&gt;&lt;!--BELFIUSAC--&gt;<strong> le &lt;!--DATE--&gt;</strong></span>. Votre programme d&eacute;butera &agrave;</span><span style="color: #002649; font-family: calibri; font-size: 11pt;">&nbsp;<strong>&lt;!--TIME--&gt;</strong> et durera&nbsp;</span><span style="color: #002649; font-family: calibri; font-size: 14.6667px;"><strong>&lt;!--DURATION--&gt;.</strong>&nbsp;<!--FODFINPRELOC--><!--ONLINELOC--></span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--IMPORTANT--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--ONLINEDESC--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--MAIL--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--CONTEXTLOGIN--&gt;</span><strong><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--SIMCONLOGINS--&gt;</span></strong></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--QCANDIDATELOGIN--&gt;</span></p>
<p><span style="font-size: 11pt; color: #002649; font-family: calibri;">&lt;!--SUBCATEGORIES--&gt;&lt;!--BPOST--&gt;&lt;!--BELFIUSBODY--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 14.6667px;">&lt;!--HELP--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--CV--&gt;</span></p>
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--HOERABOEK--&gt;</span></p>
<!--ONLINEPARK-->
<p><span style="color: #002649; font-family: calibri; font-size: 11pt;"><span style="color: #002649; font-family: calibri; font-size: 11pt;">&lt;!--FODFIN--&gt;Cordialement,</span><br /></span></p>
<div id="ConnectiveDocSignExtentionInstalled" data-extension-version="1.0.4">&nbsp;</div>'
where languageid = (SELECT TOP(1) Id FROM dbo.Language WHERE Code = 'FR')
	and MailTemplateId = (SELECT TOP(1) Id FROM dbo.MailTemplate WHERE Code = 'CANDINVITE')
GO