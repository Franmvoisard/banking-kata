namespace Kata;

public interface ITransactionPrinter
{
    void Print(IEnumerable<Transaction> transaction);
}