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
        var moneyRepository = Substitute.For<IMoneyRepository>();
        var transactionsRepository = new TransactionRepository();
        var transactionPrinter = Substitute.For<ITransactionPrinter>();
        var dateProvider = new DateProvider();
        var account = new Account(moneyRepository, transactionsRepository, dateProvider, transactionPrinter);
    
        account.Deposit(500);
        account.Withdraw(100); 
        
        //When
        account.PrintStatement();
        
        //Then
       Received.InOrder(() =>
           {
               console.Print("Date       | Amount | Balance");
               console.Print("24.12.2015 | +500   | 500");
               console.Print("23.8.2016  | -100   | 400");
           }
           );
    }
}