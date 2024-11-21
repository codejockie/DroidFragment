using Android.Text;
using Java.Lang;
using Exception = System.Exception;

namespace TryFragment;

internal class RangeFilter : Java.Lang.Object, IInputFilter
{
    private readonly int _min, _max;

    public RangeFilter(int min, int max)
    {
        _min = min;
        _max = max;
        if (_min > _max) (_min, _max) = (_max, _min);
    }

    public RangeFilter(Range range)
    {
        _min = range.Start.Value;
        _max = range.End.Value;
        if (_min > _max) (_min, _max) = (_max, _min);
    }

    public ICharSequence? FilterFormatted(ICharSequence? source, int start, int end, ISpanned? dest, int dstart, int dend)
    {

        try
        {
            var val = dest!.ToString().Insert(dstart, source!.ToString());
            int input = int.Parse(val);
            if (IsInRange(_min, _max, input)) return null;
        }
        catch (FormatException)
        {
            return new Java.Lang.String(string.Empty);
        }
        catch (Exception)
        {
            // do nothing
        }

        return new Java.Lang.String(string.Empty);
    }

    private static bool IsInRange(int min, int max, int input) => max > min
            ? input >= min && input <= max
            : input >= max && input <= min;
}
