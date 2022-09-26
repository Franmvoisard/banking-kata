using NUnit.Framework;

namespace Kata.Tests.Domain;

public class AccountShould
{
    [Test]
    public void Increment_Money_When_Deposit()
    {
        //Given
        var moneyRepository = new InMemoryMoneyRepository();
        var account = new Account(moneyRepository);
        const int expectedAmount = 100;

        //When
        account.Deposit(100);

        //Then
        var result = account.GetFunds();
        Assert.AreEqual(expectedAmount, result);
    }
    
    [Test]
    public void Decrement_Money_When_Withdraw()
    {
        //Given
        var moneyRepository = new InMemoryMoneyRepository(300);
        var account = new Account(moneyRepository);
        const int expectedAmount = 100;
        const int withdrawAmount = 200;

        //When
        account.Withdraw(withdrawAmount);

        //Then
        var result = account.GetFunds();
        Assert.AreEqual(expectedAmount, result);
    }
}