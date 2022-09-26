namespace Kata;

public class TransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactions = new();

    public IEnumerable<Transaction> GetAll()
    {
        return _transactions;
    }

    public void Add(Transaction transaction)
    {
        _transactions.Add(transaction);
    }
}