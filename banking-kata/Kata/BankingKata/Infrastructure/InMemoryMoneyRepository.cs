namespace Kata;

public class InMemoryMoneyRepository : IMoneyRepository
{
    private int _money;

    public InMemoryMoneyRepository(int startingMoney = 0)
    {
        _money = startingMoney;
    }
    public void Add(int amount)
    {
        _money += amount;
    }

    public int Get()
    {
        return _money;
    }

    public void Remove(int amount)
    {
        _money -= amount;
    }
}