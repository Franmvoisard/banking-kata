using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Kata.Tests.Domain;

public class AccountShould
{
    private InMemoryMoneyRepository _moneyRepository;
    private TransactionRepository _transactionsRepository;
    private DateProvider _dateProvider;
    private Account _account;
    private ITransactionPrinter _transactionPrinter;

    [SetUp]
    public void Setup()
    {
        _moneyRepository = new InMemoryMoneyRepository();
        _transactionsRepository = new TransactionRepository();
        _dateProvider = new DateProvider();
        _transactionPrinter = new TransactionPrinter();
        _account = new Account(_moneyRepository, _transactionsRepository, _dateProvider, _transactionPrinter);
    }

    [Test]
    public void Increment_Money_When_Deposit()
    {
        //Given
        const int expectedAmount = 100;

        //When
        _account.Deposit(100);

        //Then
        var result = _moneyRepository.Get();
        Assert.AreEqual(expectedAmount, result);
    }
    
    [Test]
    public void Decrement_Money_When_Withdraw()
    {
        //Given
        _moneyRepository = new InMemoryMoneyRepository(300);
        _account = new Account(_moneyRepository, _transactionsRepository, _dateProvider, _transactionPrinter);
        const int expectedAmount = 100;
        const int withdrawAmount = 200;

        //When
        _account.Withdraw(withdrawAmount);

        //Then
        var result = _moneyRepository.Get();
        Assert.AreEqual(expectedAmount, result);
    }

    [Test]
    public void Register_Deposit_Transaction()
    {
        const int expectedAmountOfTransactions = 1;
        const int depositAmount = 100;
        var expectedTransaction = new Transaction(_dateProvider.GetDate(), TransactionType.Deposit, depositAmount);
        
        //When
        _account.Deposit(depositAmount);
        
        //Then
        var transactions = _transactionsRepository.GetAll().ToArray();
        Assert.AreEqual(expectedTransaction, transactions.First());
        Assert.AreEqual(expectedAmountOfTransactions,transactions.Length);
    }

    [Test]
    public void Register_Withdraw_Transaction()
    {
        const int expectedAmountOfTransactions = 1;
        const int withdrawAmount = 100;
        var expectedTransaction = new Transaction(_dateProvider.GetDate(), TransactionType.Withdraw, withdrawAmount);
        
        //When
        _account.Withdraw(withdrawAmount);
        
        //Then
        var transactions = _transactionsRepository.GetAll().ToArray();
        Assert.AreEqual(expectedTransaction, transactions.First());
        Assert.AreEqual(expectedAmountOfTransactions,transactions.Length);
    }

    [Test]
    public void Print_Statements()
    {
        //Given
        var transactionPrinter = Substitute.For<ITransactionPrinter>();
        _account = new Account(_moneyRepository, _transactionsRepository, _dateProvider, transactionPrinter);
        _account.Deposit(100);
        _account.Withdraw(100);

        //When
        _account.PrintStatement();
        
        //Then
        var transactions = _transactionsRepository.GetAll().ToArray();
        Received.InOrder(() =>
        {
            transactionPrinter.Print(transactions[0]);
            transactionPrinter.Print(transactions[1]);
        });
    }
}