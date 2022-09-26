namespace Kata;

public class Account
{
    private readonly IMoneyRepository _moneyRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateProvider _dateProvider;
    public Account(IMoneyRepository moneyRepository, ITransactionRepository transactionRepository, IDateProvider dateProvider)
    {
        _moneyRepository = moneyRepository;
        _transactionRepository = transactionRepository;
        _dateProvider = dateProvider;
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
    }

    public int GetFunds()
    {
        return _moneyRepository.Get();
    }
}

public class DateProvider : IDateProvider
{
    public DateTime GetDate()
    {
        return DateTime.Today;
    }
}

public interface IDateProvider
{
    DateTime GetDate();
}

public enum TransactionType
{
    Deposit,
    Withdraw
}