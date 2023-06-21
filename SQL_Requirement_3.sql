
/*
 3)	Create a script (Insert / Update / Delete) for record in Animal table.
*/

USE [STGenetics]
GO

CREATE PROCEDURE [dbo].[ANIMAL_MANAGEMENT_SP]
(
	@AnimalId INTEGER = 0,
	@Name VARCHAR(100),
	@Breed VARCHAR(50),
	@BirthDate DATETIME,
	@Sex VARCHAR(10),
	@Price DECIMAL(18,2),
	@Status VARCHAR(10),
	@ActionType NVARCHAR(20) = ''
)
AS
BEGIN
    IF @ActionType = 'INSERT'
	BEGIN
		INSERT INTO [dbo].[Animal]
		([Name], [Breed], [BirthDate], [Sex], [Price], [Status])
		VALUES
		(@Name, @Breed, @BirthDate, @Sex, @Price, @Status)
	END

	IF @ActionType = 'UPDATE'
	BEGIN
		UPDATE [dbo].[Animal]
		SET [Name] = @Name
		,[Breed] = @Breed
		,[BirthDate] = @BirthDate
		,[Sex] = @Sex
		,[Price] = @Price
		,[Status] = @Status
		WHERE [AnimalId] = @AnimalId
	END

	IF @ActionType = 'DELETE'
	BEGIN
		DELETE [dbo].[Animal]
		WHERE [AnimalId] = @AnimalId
	END
END
GO