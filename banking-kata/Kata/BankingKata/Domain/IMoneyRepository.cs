namespace Kata;

public interface IMoneyRepository
{
    void Add(int amount);
    int Get();
    void Remove(int amount);
}