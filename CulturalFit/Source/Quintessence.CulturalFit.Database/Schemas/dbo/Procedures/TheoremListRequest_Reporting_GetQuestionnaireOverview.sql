CREATE PROCEDURE [dbo].[TheoremListRequest_Reporting_GetQuestionnaireOverview] 
      @TheoremListRequestId UNIQUEIdENTIFIER,
      @LanguageId INT
AS
BEGIN
      DECLARE     @Result AS TABLE
            (
                  Id UNIQUEIdENTIFIER, 
                  MostQuote VARCHAR(MAX), 
                  LeastQuote VARCHAR(MAX), 
                  [Type] INT
            )
      DECLARE @Most AS TABLE
            (
                  RowId INT IdENTITY(1,1), 
                  Id UNIQUEIdENTIFIER, 
                  MostQuote VARCHAR(MAX), 
                  [Type] INT
            )
      DECLARE @Least AS TABLE
            (
                  RowId INT IdENTITY(1,1), 
                  Id UNIQUEIdENTIFIER, 
                  LeastQuote VARCHAR(MAX), 
                  [Type] INT
            )

      INSERT INTO @Most
            SELECT      TheoremList.Id, 
                        TheoremTranslation.Quote, 
                        TheoremList.TheoremListTypeId
            FROM  TheoremList
                  INNER JOIN  Theorem
                        ON TheoremList.Id = Theorem.TheoremListId
                        AND (Theorem.IsMostApplicable = 1)
                        
                  INNER JOIN  TheoremTranslation 
                        ON Theorem.Id = TheoremTranslation.TheoremId
                        AND languageId = @LanguageId
            WHERE TheoremList.TheoremListRequestId = @TheoremListRequestId
            ORDER BY    TheoremList.TheoremListTypeId

      INSERT INTO @Least
            SELECT      TheoremList.Id, 
                        TheoremTranslation.Quote, 
                        TheoremList.TheoremListTypeId
            FROM  TheoremList
                  INNER JOIN  Theorem
                        ON TheoremList.Id = Theorem.TheoremListId
                        AND (Theorem.IsLeastApplicable = 1)
                  INNER JOIN  TheoremTranslation 
                        ON Theorem.Id = TheoremTranslation.TheoremId
                        AND languageId = @LanguageId
            WHERE TheoremList.TheoremListRequestId = @TheoremListRequestId
            ORDER BY    TheoremList.TheoremListTypeId

      SELECT      m.Id, 
                  m.MostQuote, 
                  l.LeastQuote, 
                  m.[Type]
      FROM @Most m
            INNER JOIN @Least l 
                  ON l.RowId = m.RowId
      ORDER BY m.Id

END
