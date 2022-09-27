namespace Kata;

public class Console : IConsole
{
    public void Print(string output)
    {
        System.Console.WriteLine(output);
    }
}