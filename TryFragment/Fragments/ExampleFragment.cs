using Android.Views;
using Google.Android.Material.DatePicker;
using Google.Android.Material.TextField;
using static TryFragment.ViewHelpers;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace TryFragment.Fragments;

public class ExampleFragment() : Fragment(Resource.Layout.example_fragment)
{
    public const string NameTag = nameof(ExampleFragment);

    public override void OnViewCreated(View view, Bundle? savedInstanceState)
    {
        base.OnViewCreated(view, savedInstanceState);

        if (Arguments != null)
        {
            var firstName = Arguments.GetString("firstName", "");
            var lastName = Arguments.GetString("lastName", "");
            SetInputText(view, Resource.Id.edit_text_first_name, firstName);
            SetInputText(view, Resource.Id.edit_text_last_name, lastName);
        }

        AddButtonClickHandler(view, Resource.Id.button_save, OnSaveClick);
        AddButtonClickHandler(view, Resource.Id.button_cancel, OnCancelClick);
        AddButtonClickHandler(view, Resource.Id.button_production_date_toggle, OnProductionDateToggle);
        SetTextInputLayoutError(view, Resource.Id.text_input_layout_license_number, "License number is required");
        SetTextInputClickHandler(view, Resource.Id.text_input_expiration_date, OnExpirationDateTextInputClick);
        AddCheckboxCheckHandler(view, Resource.Id.checkbox_no_expiration, OnCheckChanged);
        SetTextInputClickHandler(view, Resource.Id.text_input_production_date, OnProductionDateTextInputClick);
        SetTextInputFilters(view, Resource.Id.text_input_production_week, [new RangeFilter(1, 53)]);
    }

    // A fragment must have only a constructor with no arguments.
    public static ExampleFragment NewInstance() => new();

    public static ExampleFragment NewInstance(string firstName, string lastName)
    {
        var args = new Bundle();
        args.PutString(nameof(firstName), firstName);
        args.PutString(nameof(lastName), lastName);
        return new ExampleFragment { Arguments = args };
    }

    public void DoSomething(string param)
    {
        // do something in fragment
    }

    private void OnSaveClick(object? sender, EventArgs e)
    {
        Toast.MakeText(Activity, "Action saved!", ToastLength.Long)?.Show();
    }

    private void OnCancelClick(object? sender, EventArgs e)
    {
        Toast.MakeText(Activity, "Action cancelled!", ToastLength.Long)?.Show();
    }

    private void OnExpirationDateTextInputClick(object? sender, EventArgs e)
    {
        if (sender is TextInputEditText tie)
        {
            var datePicker = GetDatePicker(tie.Text, "Select expiration date");
            var positiveClickListener = new OnPositiveButtonClickListener();
            positiveClickListener.DateSelected += (_, e) =>
            {
                SetInputText(View, Resource.Id.text_input_expiration_date, $"{e:MM/dd/yy}");
            };
            datePicker.AddOnPositiveButtonClickListener(positiveClickListener);
            datePicker.Show(ParentFragmentManager, null);
        }
    }

    private void OnProductionDateTextInputClick(object? sender, EventArgs e)
    {
        if (sender is TextInputEditText tie)
        {
            var datePicker = GetDatePicker(tie.Text, "Select production date");
            var positiveClickListener = new OnPositiveButtonClickListener();
            positiveClickListener.DateSelected += (_, e) =>
            {
                SetInputText(View, Resource.Id.text_input_production_date, $"{e:MM/dd/yy}");
            };
            datePicker.AddOnPositiveButtonClickListener(positiveClickListener);
            datePicker.Show(ParentFragmentManager, null);
        }
    }

    private bool _isOrdinalProductionDate;
    private void OnProductionDateToggle(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.Text = _isOrdinalProductionDate ? "STD" : "ORD";
            _isOrdinalProductionDate = !_isOrdinalProductionDate;
        }
    }

    private void OnCheckChanged(object? sender, CompoundButton.CheckedChangeEventArgs e)
    {
        ToggleTextInputLayoutState(View, Resource.Id.text_input_layout_expiration_date);
    }
}

class OnPositiveButtonClickListener : Java.Lang.Object, IMaterialPickerOnPositiveButtonClickListener
{
    public event EventHandler<DateTimeOffset>? DateSelected;

    public void OnPositiveButtonClick(Java.Lang.Object? p0)
    {
        if (p0 != null)
        {
            var milliseconds = (long)p0;
            // Java uses the UNIX epoch which starts at January 1, 1970
            var selectedDate = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
            DateSelected?.Invoke(this, selectedDate);
        }
    }
}