/*
6)	Create a script to list the quantity of animals by sex.
*/

SELECT [Sex], COUNT(0) AS QUANTITY
  FROM [STGenetics].[dbo].[Animal]
  GROUP BY Sex
