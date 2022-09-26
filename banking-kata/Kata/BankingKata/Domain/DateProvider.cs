namespace Kata;

public class DateProvider : IDateProvider
{
    public DateTime GetDate()
    {
        return DateTime.Today;
    }
}