using Android.Text;
using Android.Views;
using Google.Android.Material.DatePicker;
using Google.Android.Material.TextField;
using Java.Text;

namespace TryFragment;

internal class ViewHelpers
{
    public static void SetText(View? view, int id, string text)
    {
        var textView = view?.FindViewById<TextView>(id);
        if (textView != null) textView.Text = text;
    }

    public static void SetInputText(View? view, int id, string text)
    {
        var textView = view?.FindViewById<TextInputEditText>(id);
        if (textView != null) textView.Text = text;
    }

    public static void AddButtonClickHandler(View? view, int id, EventHandler handler)
    {
        var button = view?.FindViewById<Button>(id);
        if (button != null) button.Click += handler;
    }

    public static void SetTextInputLayoutError(View? view, int id, string error)
    {
        var layout = view?.FindViewById<TextInputLayout>(id);
        if (layout != null) layout.Error = error;
    }

    public static void SetTextInputClickHandler(View? view, int id, EventHandler handler)
    {
        var input = view?.FindViewById<TextInputEditText>(id);
        if (input != null) input.Click += handler;
    }

    public static void ToggleTextInputState(View? view, int id)
    {
        var input = view?.FindViewById<TextInputEditText>(id);
        if (input != null) input.Enabled = !input.Enabled;
    }

    public static void ToggleTextInputLayoutState(View? view, int id)
    {
        var layout = view?.FindViewById<TextInputLayout>(id);
        if (layout != null) layout.Enabled = !layout.Enabled;
    }

    public static void SetDateInputOnFocusAction(View? view, int id, EventHandler<View.FocusChangeEventArgs> handler)
    {
        var input = view?.FindViewById<TextInputEditText>(id);
        if (input != null) input.FocusChange += handler;
    }

    public static void AddCheckboxCheckHandler(View? view, int id, EventHandler<CompoundButton.CheckedChangeEventArgs> handler)
    {
        var checkbox = view?.FindViewById<CheckBox>(id);
        if (checkbox != null) checkbox.CheckedChange += handler;
    }

    public static void ToggleViewVisibility(View? view, int id)
    {
        var v = view?.FindViewById<View>(id);
        if (v != null) v.Visibility = ViewStates.Gone;
    }

    public static void SetTextInputFilters(View? view, int id, IInputFilter[] filters)
    {
        var input = view?.FindViewById<TextInputEditText>(id);
        input?.SetFilters(filters);
    }

    public static MaterialDatePicker GetDatePicker(string? value, string? titleText)
    {
        var parsed = DateTime.TryParseExact(value, "MM/dd/yy", default, default, out var dateTime);

        return MaterialDatePicker.Builder.DatePicker()
            .SetTitleText(titleText)
            // Start the picker in text input mode
            .SetInputMode(MaterialDatePicker.InputModeText)
            .SetTextInputFormat(new SimpleDateFormat("MM/dd/yy"))
            .SetSelection(parsed ? new DateTimeOffset(dateTime).ToUnixTimeMilliseconds() : null)
            .Build();
    }
}
