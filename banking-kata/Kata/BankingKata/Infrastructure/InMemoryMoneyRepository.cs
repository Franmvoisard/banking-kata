namespace Kata;

public class InMemoryMoneyRepository : IMoneyRepository
{
    private int _money;

    public void Add(int amount)
    {
        _money += amount;
    }

    public int Get()
    {
        return _money;
    }
}