using System;
using NSubstitute;
using NUnit.Framework;

namespace Kata.Tests.Domain;

public class TransactionPrinterShould
{
    private TransactionPrinter _transactionPrinter;
    private IConsole _console = Substitute.For<IConsole>();
    private IDateProvider _dateProvider = Substitute.For<IDateProvider>();
    private readonly IMoneyRepository _moneyRepository = Substitute.For<IMoneyRepository>();
    
    [SetUp]
    public void Setup()
    {
        _console = Substitute.For<IConsole>();
        _dateProvider = Substitute.For<IDateProvider>();
        _transactionPrinter = new TransactionPrinter(_console);
    }

    [Test]
    public void Print_A_Deposit()
    {
        //Given
        const int depositAmount = 300;
        _dateProvider = Substitute.For<IDateProvider>();
        _dateProvider.GetDate().Returns(new DateTime(2022, 09, 26));
        _moneyRepository.Get().Returns(depositAmount);
        var transaction = new Transaction(_dateProvider.GetDate(), TransactionType.Deposit, depositAmount, _moneyRepository.Get());
        
        //When
        _transactionPrinter.Print(new []{ transaction });
        
        //Then
        _console.Received(1).Print("26.9.2022|+300|300");
    }
    
    [Test]
    public void Print_A_Withdraw()
    {
        //Given
        const int withdrawAmount = 300;
        _dateProvider = Substitute.For<IDateProvider>();
        _dateProvider.GetDate().Returns(new DateTime(2022, 10, 26));
        _moneyRepository.Get().Returns(withdrawAmount);
        var transaction = new Transaction(_dateProvider.GetDate(), TransactionType.Withdraw, withdrawAmount, _moneyRepository.Get());
        
        //When
        _transactionPrinter.Print(new []{ transaction });
        
        //Then
        _console.Received(1).Print("26.10.2022|-300|300");
    }

    [Test]
    public void Print_Right_Balance()
    {
        //Given
        const int depositAmount = 500;
        const int withdrawAmount = 300;
        
        _dateProvider = Substitute.For<IDateProvider>();
        var firstTransactionDate = new DateTime(2022, 10, 26);
        _dateProvider.GetDate().Returns(firstTransactionDate);
        _moneyRepository.Get().Returns(withdrawAmount);
        
        var aDepositTransaction = CreateTransaction(_dateProvider, TransactionType.Deposit, depositAmount, 500);
        var aWithdrawTransaction = CreateTransaction(_dateProvider, TransactionType.Withdraw, withdrawAmount, 200);

        var transactions = new[]{aDepositTransaction, aWithdrawTransaction};
        
        //When
        _transactionPrinter.Print(transactions);
        
        //Then
        Received.InOrder(() =>
        {
         _console.Received(1).Print("26.10.2022|+500|500");
         _console.Received(1).Print("26.10.2022|-300|200");
        });
    }

    private Transaction CreateTransaction(IDateProvider provider, TransactionType type, int transactionAmount, int balance)
    {
        return new Transaction(provider.GetDate(), type, transactionAmount, balance);
    }
}