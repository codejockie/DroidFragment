using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using TryFragment.Fragments;

namespace TryFragment;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : AppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        var newButton = FindViewById<MaterialButton>(Resource.Id.button_new);
        var editButton = FindViewById<MaterialButton>(Resource.Id.button_edit);

        if (newButton != null) newButton.Click += OnNewButtonClick;
        if (editButton != null) editButton.Click += OnEditButtonClick;

        if (savedInstanceState == null)
        {
            // Pass arguments to fragment
            SupportFragmentManager.BeginTransaction()
                .SetReorderingAllowed(true)
                .Add(Resource.Id.fragment_container_view, ExampleFragment.NewInstance(), ExampleFragment.NameTag)
                .Commit();
        }
    }

    private void OnEditButtonClick(object? sender, EventArgs e)
    {
        SupportFragmentManager
            .BeginTransaction()
            .SetReorderingAllowed(true)
            .Replace(Resource.Id.fragment_container_view, ExampleFragment.NewInstance("Kennedy", "Nwaorgu"))
            .Commit();
    }

    private void OnNewButtonClick(object? sender, EventArgs e)
    {
        SupportFragmentManager
            .BeginTransaction()
            .SetReorderingAllowed(true)
            .Replace(Resource.Id.fragment_container_view, ExampleFragment.NewInstance())
            .Commit();
    }
}