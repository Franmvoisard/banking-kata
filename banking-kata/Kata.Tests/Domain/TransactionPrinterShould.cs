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
        _transactionPrinter = new TransactionPrinter(_moneyRepository, _console);
    }

    [Test]
    public void Print_A_Deposit()
    {
        //Given
        const int depositAmount = 300;
        _dateProvider = Substitute.For<IDateProvider>();
        _dateProvider.GetDate().Returns(new DateTime(2022, 09, 26));
        _moneyRepository.Get().Returns(depositAmount);
        
        //When
        _transactionPrinter.Print(new Transaction(_dateProvider.GetDate(), TransactionType.Deposit, depositAmount));
        
        //Then
        _console.Received(1).Print("26/09/2022|+300|300");
    }
}