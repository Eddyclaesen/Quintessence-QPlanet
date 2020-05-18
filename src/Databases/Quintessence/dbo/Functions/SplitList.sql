CREATE FUNCTION [dbo].[SplitList]
(
	@List NVARCHAR(2000),
	@Separater CHAR(1)
)  
RETURNS @RtnValue TABLE 
(
	Value NVARCHAR(100)
) 
AS  
BEGIN
	WHILE (CHARINDEX(@Separater, @List)>0)
	BEGIN 
	INSERT INTO @RtnValue (value)
	SELECT		Value = LTRIM(RTRIM(SUBSTRING(@List, 1, CHARINDEX(@Separater, @List) - 1))) 
	SET			@List = SUBSTRING(@List, CHARINDEX(@Separater, @List) + LEN(@Separater), LEN(@List))
	END 

	INSERT INTO @RtnValue (Value)
	SELECT Value = LTRIM(RTRIM(@List))
	RETURN
END