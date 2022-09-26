namespace Kata;

public struct Transaction
{
    public DateTime Date { get; }
    public TransactionType Deposit { get; }
    public int Amount { get; }

    public Transaction(DateTime date, TransactionType deposit, int amount)
    {
        Date = date;
        Deposit = deposit;
        Amount = amount;
    }
}