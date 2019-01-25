CREATE TRIGGER [Trigger]
	ON [dbo].[TransactionItems]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	UPDATE Items
		SET Stock = i.Stock- t.quantity
	FROM Items i JOIN inserted t ON i.id=t.Items_Id;
	END
