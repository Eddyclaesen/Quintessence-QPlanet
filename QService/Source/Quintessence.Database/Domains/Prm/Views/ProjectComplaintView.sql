﻿CREATE VIEW dbo.ProjectComplaintView
AS
SELECT        dbo.ProjectComplaint.Id, dbo.ProjectComplaint.CrmProjectId, dbo.ProjectComplaint.Subject, dbo.ProjectComplaint.ComplaintDate, 
                         dbo.ProjectComplaint.ComplaintDetails, dbo.ProjectComplaint.ComplaintStatusTypeId, dbo.ProjectComplaint.ComplaintSeverityTypeId, 
                         dbo.ProjectComplaint.ComplaintTypeId, dbo.ProjectComplaint.FollowUp, dbo.ProjectComplaint.Audit_CreatedBy, dbo.ProjectComplaint.Audit_CreatedOn, 
                         dbo.ProjectComplaint.Audit_ModifiedBy, dbo.ProjectComplaint.Audit_ModifiedOn, dbo.ProjectComplaint.Audit_DeletedBy, 
                         dbo.ProjectComplaint.Audit_DeletedOn, dbo.ProjectComplaint.Audit_IsDeleted, dbo.ProjectComplaint.Audit_VersionId, dbo.ProjectComplaint.SubmitterId, 
                         dbo.ComplaintStatusType.Name AS ComplaintStatusTypeName, dbo.ComplaintSeverityType.Name AS ComplaintSeverityTypeName, 
                         dbo.ComplaintType.Name AS ComplaintTypeName, dbo.[User].FirstName + ' ' + dbo.[User].LastName AS SubmitterName
FROM            dbo.ProjectComplaint WITH (NOLOCK) INNER JOIN
                         dbo.[User] ON dbo.ProjectComplaint.SubmitterId = dbo.[User].Id LEFT OUTER JOIN
                         dbo.ComplaintStatusType ON dbo.ComplaintStatusType.Id = dbo.ProjectComplaint.ComplaintStatusTypeId LEFT OUTER JOIN
                         dbo.ComplaintSeverityType ON dbo.ComplaintSeverityType.Id = dbo.ProjectComplaint.ComplaintSeverityTypeId LEFT OUTER JOIN
                         dbo.ComplaintType ON dbo.ComplaintType.Id = dbo.ProjectComplaint.ComplaintTypeId
WHERE        (dbo.ProjectComplaint.Audit_IsDeleted = 0)

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProjectComplaintView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'n = 2610
         Alias = 2805
         Table = 2430
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProjectComplaintView';




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
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProjectComplaint"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 14
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ComplaintStatusType"
            Begin Extent = 
               Top = 6
               Left = 298
               Bottom = 119
               Right = 468
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ComplaintSeverityType"
            Begin Extent = 
               Top = 120
               Left = 298
               Bottom = 233
               Right = 468
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ComplaintType"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 208
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
         Colum', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ProjectComplaintView';



