namespace Kata;

public class Account
{
    private readonly IMoneyRepository _moneyRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateProvider _dateProvider;
    private readonly ITransactionPrinter _transactionPrinter;

    public Account(IMoneyRepository moneyRepository, ITransactionRepository transactionRepository, IDateProvider dateProvider, ITransactionPrinter transactionPrinter)
    {
        _moneyRepository = moneyRepository;
        _transactionRepository = transactionRepository;
        _dateProvider = dateProvider;
        _transactionPrinter = transactionPrinter;
    }

    public void Deposit(int amount)
    {
        _moneyRepository.Add(amount);
        var date = _dateProvider.GetDate();
        var transaction = new Transaction(date, TransactionType.Deposit, amount);
        _transactionRepository.Add(transaction);
    }

    public void Withdraw(int amount)
    {
        _moneyRepository.Remove(amount); 
        var date = _dateProvider.GetDate();
        var transaction = new Transaction(date, TransactionType.Withdraw, amount);
        _transactionRepository.Add(transaction);
    }

    public void PrintStatement()
    {
        foreach (var transaction in _transactionRepository.GetAll())
        {
            _transactionPrinter.Print(transaction);
        }
    }
}

public enum TransactionType
{
    Deposit,
    Withdraw
}