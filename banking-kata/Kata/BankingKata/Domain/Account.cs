namespace Kata;

public class Account
{
    private readonly IMoneyRepository _moneyRepository;

    public Account(IMoneyRepository moneyRepository)
    {
        _moneyRepository = moneyRepository;
    }

    public void Deposit(int amount)
    {
        _moneyRepository.Add(amount);
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

public interface IMoneyRepository
{
    void Add(int amount);
    int Get();
    void Remove(int amount);
}