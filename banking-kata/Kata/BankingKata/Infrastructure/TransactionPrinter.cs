using System.Text;

namespace Kata;

public class TransactionPrinter : ITransactionPrinter
{
    private const char Separator = '|';
    private readonly IMoneyRepository _moneyRepository;
    private readonly IConsole _console;

    public TransactionPrinter(IMoneyRepository moneyRepository, IConsole console)
    {
        _moneyRepository = moneyRepository;
        _console = console;
    }

    public void Print(IEnumerable<Transaction> transactions)
    {
        PrintHeaders();
        PrintAllTransactions(transactions);
    }

    private void PrintAllTransactions(IEnumerable<Transaction> transactions)
    {
        transactions.ToList().ForEach(PrintTransaction);
    }

    private void PrintTransaction(Transaction transaction)
    {
        var stringBuilder = new StringBuilder();
        var date = transaction.Date.ToString("dd.M.yyyy");
        var transactionSign = GetAmountSign(transaction);
            
        var output = stringBuilder
            .Append(date)
            .Append(Separator)
            .Append(transactionSign)
            .Append(transaction.Amount)
            .Append(Separator)
            .Append(_moneyRepository.Get());
        
        _console.Print(output.ToString());
    }

    private void PrintHeaders()
    {
        _console.Print("Date       | Amount | Balance");
    }

    private static char GetAmountSign(Transaction transaction)
    {
        return transaction.Type == TransactionType.Deposit ? '+' : '-';
    }
}