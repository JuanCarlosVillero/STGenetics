/*
5)	Create a script to list animals older than 2 years and female, sorted by name.
*/

USE [STGenetics]
GO

SELECT [AnimalId]
      ,[Name]
      ,[Breed]
      ,[BirthDate]
      ,[Sex]
      ,[Price]
      ,[Status]
  FROM [STGenetics].[dbo].[Animal]
  WHERE DATEDIFF(YEAR, BirthDate, GETDATE()) > 2