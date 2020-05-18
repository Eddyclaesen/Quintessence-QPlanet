CREATE VIEW dbo.ProposalView
AS
SELECT        dbo.Proposal.Id, dbo.Proposal.Name, dbo.Proposal.Description, dbo.Proposal.ContactId, dbo.Proposal.BusinessDeveloperId, dbo.Proposal.ExecutorId, dbo.Proposal.DateReceived, dbo.Proposal.Deadline, 
                         dbo.Proposal.DateSent, dbo.Proposal.DateWon, dbo.Proposal.PriceEstimation, dbo.Proposal.Prognosis, dbo.Proposal.FinalBudget, dbo.Proposal.StatusCode, dbo.Proposal.StatusReason, 
                         dbo.Proposal.WrittenProposal, dbo.Proposal.Audit_CreatedBy, dbo.Proposal.Audit_CreatedOn, dbo.Proposal.Audit_ModifiedBy, dbo.Proposal.Audit_ModifiedOn, dbo.Proposal.Audit_DeletedBy, 
                         dbo.Proposal.Audit_DeletedOn, dbo.Proposal.Audit_IsDeleted, dbo.Proposal.Audit_VersionId, dbo.CrmContactView.Name AS ContactName, dbo.CrmContactView.Department AS ContactDepartment, 
                         BusinessDeveloperUser.FirstName AS BusinessDeveloperFirstName, BusinessDeveloperUser.LastName AS BusinessDeveloperLastName, ExecutorUser.FirstName AS ExecutorFirstName, 
                         ExecutorUser.LastName AS ExecutorLastName
FROM            dbo.Proposal WITH (NOLOCK) INNER JOIN
                         dbo.CrmContactView ON dbo.CrmContactView.Id = dbo.Proposal.ContactId LEFT OUTER JOIN
                         dbo.UserView AS BusinessDeveloperUser ON BusinessDeveloperUser.Id = dbo.Proposal.BusinessDeveloperId LEFT OUTER JOIN
                         dbo.UserView AS ExecutorUser ON ExecutorUser.Id = dbo.Proposal.ExecutorId
WHERE        (dbo.Proposal.Audit_IsDeleted = 0)

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
         Begin Table = "Proposal"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "CrmContactView"
            Begin Extent = 
               Top = 6
               Left = 273
               Bottom = 136
               Right = 471
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BusinessDeveloperUser"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExecutorUser"
            Begin Extent = 
               Top = 138
               Left = 263
               Bottom = 268
               Right = 450
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
         Column = 1440
         Alias = 900
         Table = 1170
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProposalView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProposalView';

