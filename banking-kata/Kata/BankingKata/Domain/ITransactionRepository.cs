namespace Kata;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetAll();
    void Add(Transaction transaction);
}