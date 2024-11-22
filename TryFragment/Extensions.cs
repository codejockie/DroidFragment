namespace TryFragment;

internal static class Extensions
{
    public static string ToOrdinal(this DateTime dt) => $"{dt:yy}{dt.DayOfYear:000}";

    public static DateTime GetDateByWeekNumber(int week, int year) =>
        new DateTime(year, 1, 1).AddDays(7 * week - 7) switch
        {
            var result when result.Year == year => result,
            var result => throw new Exception($"Week #{week} of {year} results in {result}")
        };

    public static int GetWeekNumber(this DateTime date) => date.DayOfYear / 7 + 1;
}
