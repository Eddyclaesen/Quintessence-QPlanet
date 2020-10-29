
CREATE VIEW [dbo].[ProjectCandidateView]
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
    dbo.CandidateView.QCandidateUserId AS CandidateQCandidateUserId
FROM
	dbo.ProjectCandidate WITH (NOLOCK)
	INNER JOIN dbo.CandidateView
		ON dbo.ProjectCandidate.CandidateId = dbo.CandidateView.Id
	LEFT OUTER JOIN dbo.CrmAppointmentView
		ON dbo.CrmAppointmentView.Id = dbo.ProjectCandidate.CrmCandidateAppointmentId
WHERE
	(dbo.ProjectCandidate.Audit_IsDeleted = 0)

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProjectCandidate"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 47
         End
         Begin Table = "CandidateView"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "CrmAppointmentView"
            Begin Extent = 
               Top = 138
               Left = 263
               Bottom = 267
               Right = 447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3270
         Alias = 3840
         Table = 5790
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProjectCandidateView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProjectCandidateView';

