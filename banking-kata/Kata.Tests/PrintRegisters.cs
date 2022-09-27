using System;
using NUnit.Framework;
using NSubstitute;
namespace Kata.Tests;

public class PrintRegisters
{
    [Test]
    public void Print_Statements_In_Order()
    {
        //Given
        var console = Substitute.For<IConsole>();
        var moneyRepository = new InMemoryMoneyRepository();
        var transactionsRepository = new TransactionRepository();
        var transactionPrinter = new TransactionPrinter(moneyRepository, console);
        var dateProvider = Substitute.For<IDateProvider>();

        var datesToReturn = new DateTime[] {
            new(2015, 12, 24),
            new(2016, 08, 23)
        };
        
        dateProvider.GetDate().Returns(datesToReturn[0], datesToReturn[1]);
        var account = new Account(moneyRepository, transactionsRepository, dateProvider, transactionPrinter);
    
        account.Deposit(500);
        account.Withdraw(100); 
        
        //When
        account.PrintStatements();
        
        //Then
       Received.InOrder(() =>
           {
               console.Print("Date       | Amount | Balance");
               console.Print("24.12.2015 | +500   | 500");
               console.Print("23.8.2016  | -100   | 400");
           });
    }
}