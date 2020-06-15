CREATE VIEW dbo.DictionaryIndicatorMatrixEntryView
AS
SELECT        dbo.DictionaryIndicatorView.Id, dbo.DictionaryIndicatorView.Name AS DictionaryIndicatorName, dbo.DictionaryIndicatorView.[Order] AS DictionaryIndicatorOrder, 
                         dbo.DictionaryLevelView.Id AS DictionaryLevelId, dbo.DictionaryLevelView.[Level] AS DictionaryLevelLevel, dbo.DictionaryLevelView.Name AS DictionaryLevelName,
                          dbo.DictionaryCompetenceView.Id AS DictionaryCompetenceId, dbo.DictionaryCompetenceView.Name AS DictionaryCompetenceName, 
                         dbo.DictionaryCompetenceView.[Order] AS DictionaryCompetenceOrder, dbo.DictionaryClusterView.Id AS DictionaryClusterId, 
                         dbo.DictionaryClusterView.Name AS DictionaryClusterName, dbo.DictionaryClusterView.[Order] AS DictionaryClusterOrder, dbo.Dictionary.Id AS DictionaryId, 
                         dbo.Dictionary.Name AS DictionaryName, dbo.DictionaryIndicatorView.Color AS DictionaryIndicatorColor
FROM            dbo.DictionaryIndicatorView INNER JOIN
                         dbo.DictionaryLevelView ON dbo.DictionaryLevelView.Id = dbo.DictionaryIndicatorView.DictionaryLevelId INNER JOIN
                         dbo.DictionaryCompetenceView ON dbo.DictionaryCompetenceView.Id = dbo.DictionaryLevelView.DictionaryCompetenceId INNER JOIN
                         dbo.DictionaryClusterView ON dbo.DictionaryClusterView.Id = dbo.DictionaryCompetenceView.DictionaryClusterId INNER JOIN
                         dbo.Dictionary ON dbo.Dictionary.Id = dbo.DictionaryClusterView.DictionaryId

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
         Begin Table = "DictionaryIndicatorView"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "DictionaryLevelView"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DictionaryCompetenceView"
            Begin Extent = 
               Top = 6
               Left = 263
               Bottom = 135
               Right = 453
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DictionaryClusterView"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 399
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Dictionary"
            Begin Extent = 
               Top = 270
               Left = 263
               Bottom = 399
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
         Alias = 4290
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'DictionaryIndicatorMatrixEntryView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'DictionaryIndicatorMatrixEntryView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'DictionaryIndicatorMatrixEntryView';

