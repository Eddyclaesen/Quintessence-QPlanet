CREATE VIEW dbo.ProgramComponentView
AS
SELECT        dbo.ProgramComponent.Id, dbo.ProgramComponent.AssessmentRoomId, dbo.ProgramComponent.ProjectCandidateId, dbo.ProgramComponent.SimulationCombinationId, 
                         dbo.ProgramComponent.SimulationCombinationTypeCode, dbo.ProgramComponent.ProjectCandidateCategoryDetailTypeId, dbo.ProgramComponent.LeadAssessorUserId, 
                         dbo.ProgramComponent.CoAssessorUserId, dbo.ProgramComponent.Description, dbo.ProgramComponent.Start, dbo.ProgramComponent.[End], dbo.ProgramComponent.Audit_CreatedBy, 
                         dbo.ProgramComponent.Audit_CreatedOn, dbo.ProgramComponent.Audit_ModifiedBy, dbo.ProgramComponent.Audit_ModifiedOn, dbo.ProgramComponent.Audit_DeletedBy, 
                         dbo.ProgramComponent.Audit_DeletedOn, dbo.ProgramComponent.Audit_IsDeleted, dbo.ProgramComponent.Audit_VersionId, dbo.ProjectCandidateView.CandidateFirstName, 
                         dbo.ProjectCandidateView.CandidateLastName, dbo.CrmContactView.Name AS ContactName, dbo.AssessmentRoomView.Name AS AssessmentRoomName, 
                         dbo.AssessmentRoomView.OfficeId AS AssessmentRoomOfficeId, dbo.AssessmentRoomView.OfficeShortName AS AssessmentRoomOfficeShortName, 
                         dbo.AssessmentRoomView.OfficeFullName AS AssessmentRoomOfficeFullName, dbo.SimulationView.Name AS SimulationName, dbo.ProjectCandidateCategoryDetailTypeView.ProjectCategoryDetailTypeName, 
                         LeadAssessorUser.FirstName AS LeadAssessorUserFirstName, LeadAssessorUser.LastName AS LeadAssessorUserLastName, CoAssessorUser.FirstName AS CoAssessorUserFirstName, 
                         CoAssessorUser.LastName AS CoAssessorUserLastName
FROM            dbo.ProgramComponent WITH (NOLOCK) INNER JOIN
                         dbo.ProjectCandidateView ON dbo.ProjectCandidateView.Id = dbo.ProgramComponent.ProjectCandidateId INNER JOIN
                         dbo.AssessmentRoomView ON dbo.AssessmentRoomView.Id = dbo.ProgramComponent.AssessmentRoomId LEFT OUTER JOIN
                         dbo.SimulationCombinationView ON dbo.SimulationCombinationView.Id = dbo.ProgramComponent.SimulationCombinationId LEFT OUTER JOIN
                         dbo.SimulationView ON dbo.SimulationView.Id = dbo.SimulationCombinationView.SimulationId LEFT OUTER JOIN
                         dbo.ProjectCandidateCategoryDetailTypeView ON dbo.ProjectCandidateCategoryDetailTypeView.Id = dbo.ProgramComponent.ProjectCandidateCategoryDetailTypeId LEFT OUTER JOIN
                         dbo.UserView AS LeadAssessorUser ON LeadAssessorUser.Id = dbo.ProgramComponent.LeadAssessorUserId LEFT OUTER JOIN
                         dbo.UserView AS CoAssessorUser ON CoAssessorUser.Id = dbo.ProgramComponent.CoAssessorUserId LEFT OUTER JOIN
                         dbo.ProjectView ON dbo.ProjectCandidateView.ProjectId = dbo.ProjectView.Id LEFT OUTER JOIN
                         dbo.CrmContactView ON dbo.ProjectView.ContactId = dbo.CrmContactView.Id
WHERE        (dbo.ProgramComponent.Audit_IsDeleted = 0)

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
         Begin Table = "ProgramComponent"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 332
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProjectCandidateView"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AssessmentRoomView"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SimulationCombinationView"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 257
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SimulationView"
            Begin Extent = 
               Top = 270
               Left = 263
               Bottom = 400
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProjectCandidateCategoryDetailTypeView"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LeadAssessorUser"
            Begin Extent = 
               Top = 666
               Left = 38
        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProgramComponentView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Bottom = 796
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CoAssessorUser"
            Begin Extent = 
               Top = 666
               Left = 263
               Bottom = 796
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProjectView"
            Begin Extent = 
               Top = 6
               Left = 370
               Bottom = 136
               Right = 585
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CrmContactView"
            Begin Extent = 
               Top = 6
               Left = 623
               Bottom = 136
               Right = 821
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProgramComponentView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProgramComponentView';

