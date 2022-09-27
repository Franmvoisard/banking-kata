using System.Linq;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace Kata.Tests.Domain;

public class AccountShould
{
    private Account _account;
    private IMoneyRepository _moneyRepository;
    private ITransactionRepository _transactionsRepository;
    private IDateProvider _dateProvider;
    private ITransactionPrinter _transactionPrinter;
    private IConsole _console;

    [SetUp]
    public void Setup()
    {
        _moneyRepository = new InMemoryMoneyRepository();
        _transactionsRepository = new TransactionRepository();
        _dateProvider = new DateProvider();
        _console = new Console();
        _transactionPrinter = new TransactionPrinter(_console);
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
        var expectedTransaction = new Transaction(_dateProvider.GetDate(), TransactionType.Deposit, depositAmount, 100);
        
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
        _moneyRepository = new InMemoryMoneyRepository(300);
        _account = new Account(_moneyRepository, _transactionsRepository, _dateProvider, _transactionPrinter);
        var expectedTransaction = new Transaction(_dateProvider.GetDate(), TransactionType.Withdraw, withdrawAmount, 200);
        
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
        _account.PrintStatements();
        
        //Then
        var transactions = _transactionsRepository.GetAll();
        transactionPrinter.Received(1).Print(transactions);
    }
    
    [Test]
    public void Print_Header_Before_Statements()
    {
        //Given
        const int depositAmount = 100;
        const int expectedBalance = 100;
        
        _console = Substitute.For<IConsole>();
        _transactionPrinter = new TransactionPrinter(_console);
        _account = new Account(_moneyRepository, _transactionsRepository, _dateProvider, _transactionPrinter);
        
        _account.Deposit(depositAmount);
        var date = _dateProvider.GetDate().ToString("dd.M.yyyy");
        const char separator = '|';
        var transactionStatement = GetTransactionStatement(date, separator, TransactionType.Deposit, depositAmount, expectedBalance);
        
        //When
        _account.PrintStatements();
        
        //Then
        Received.InOrder(() =>
        {
            _console.Received(1).Print("Date       | Amount | Balance");
            _console.Received(1).Print(transactionStatement);
        });
    }

    private static string GetTransactionStatement(string date, char separator, TransactionType type, int depositAmount, int balance)
    {
        return new StringBuilder()
            .Append(date)
            .Append(separator)
            .Append(type == TransactionType.Deposit ? '+' : '-')
            .Append(depositAmount)
            .Append(separator)
            .Append(balance)
            .ToString();
    }
}