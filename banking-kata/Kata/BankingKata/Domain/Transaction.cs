namespace Kata;

public struct Transaction
{
    public DateTime Date { get; }
    public TransactionType Type { get; }
    public int Amount { get; }
    public int Balance { get; }

    public Transaction(DateTime date, TransactionType type, int amount, int balance)
    {
        Date = date;
        Type = type;
        Amount = amount;
        Balance = balance;
    }
}