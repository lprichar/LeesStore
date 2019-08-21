CREATE PROCEDURE dbo.GetIdealProductQuantity 
	@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Quantity + 1 AS IdealQuantity
	FROM Products 
	WHERE Id = @ProductId
END
GO
