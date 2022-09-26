using System;
using System.Linq;
using NUnit.Framework;

namespace Kata.Tests.Domain;

public class AccountShould
{
    [Test]
    public void Increment_Money_When_Deposit()
    {
        //Given
        var moneyRepository = new InMemoryMoneyRepository();
        var transactionsRepository = new TransactionRepository();
        var dateProvider = new DateProvider();
        var account = new Account(moneyRepository, transactionsRepository, dateProvider);
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
        var transactionsRepository = new TransactionRepository();
        var dateProvider = new DateProvider();
        var account = new Account(moneyRepository, transactionsRepository, dateProvider);
        const int expectedAmount = 100;
        const int withdrawAmount = 200;

        //When
        account.Withdraw(withdrawAmount);

        //Then
        var result = account.GetFunds();
        Assert.AreEqual(expectedAmount, result);
    }

    [Test]
    public void Register_Transaction()
    {
        const int expectedAmountOfTransactions = 1;
        const int depositAmount = 100;
        var dateProvider = new DateProvider();
        var expectedTransaction = new Transaction(dateProvider.GetDate(), TransactionType.Deposit, depositAmount);
        var transactionsRepository = new TransactionRepository();
        var moneyRepository = new InMemoryMoneyRepository();
        var account = new Account(moneyRepository, transactionsRepository, dateProvider);
        
        //When
        account.Deposit(depositAmount);
        
        //Then
        var transactions = transactionsRepository.GetAll().ToArray();
        Assert.AreEqual(expectedTransaction, transactions.First());
        Assert.AreEqual(expectedAmountOfTransactions,transactions.Length);
    }

}