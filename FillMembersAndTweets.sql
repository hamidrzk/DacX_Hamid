USE [DAC]
GO

DECLARE @Counter INT 
SET @Counter=1

DECLARE @MembersNum INT = 25000
DECLARE @TweetsNum INT = 10000 --2000000

WHILE ( @Counter <= @MembersNum)
BEGIN
PRINT CONCAT('UserCount: ', CONVERT(VARCHAR,@Counter))

INSERT INTO [dbo].[Members] 
([Email],[Name])
     VALUES (
	 CONCAT('Email' , @Counter , '@dac.test'),
	 CONCAT('Name-' , @Counter)
	 )
    SET @Counter  = @Counter  + 1
END

DECLARE @PostDate DATETIME = GETDATE(),
@TimeDiff int

SET @Counter=1
WHILE ( @Counter <= @TweetsNum)
BEGIN
--PRINT CONCAT('Tweets Added: ' , CONVERT(VARCHAR,@Counter))
SET @TimeDiff = Cast(RAND()*(60-1)+1 as int)
SET @PostDate = DATEADD(SS, @TimeDiff, @PostDate)
INSERT INTO [dbo].[Tweets] 
	([Message],[MemberId],[PostDate])
     VALUES (
		 CONCAT('Message:' , @Counter , '@dac.test')
		 ,(Cast(RAND()*(@MembersNum - 1) + 1 as int))
		 , @PostDate 
	)
SET @Counter = @Counter  + 1
END




