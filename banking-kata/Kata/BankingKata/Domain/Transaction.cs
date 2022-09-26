namespace Kata;

public struct Transaction
{
    public DateTime Date { get; }
    public TransactionType Type { get; }
    public int Amount { get; }

    public Transaction(DateTime date, TransactionType type, int amount)
    {
        Date = date;
        Type = type;
        Amount = amount;
    }
}