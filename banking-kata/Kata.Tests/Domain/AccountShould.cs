using NSubstitute;
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
}