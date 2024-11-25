using AndroidX.AppCompat.App;
using TryFragment.Fragments;

namespace TryFragment;

[Activity(Label = "RssFeedActivity", MainLauncher = true)]
public class RssFeedActivity : AppCompatActivity, MyListFragment.IOnItemSelectedListener
{

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_rssfeed);

        if (Resources.GetBoolean(Resource.Boolean.twoPaneMode))
        {
            // All good, use the fragments defined in the layout
            return;
        }

        if (savedInstanceState != null)
        {
            // Cleanup any existing fragments in case we are in detail mode
            SupportFragmentManager.ExecutePendingTransactions();
            var fragmentById = SupportFragmentManager.FindFragmentById(Resource.Id.fragment_container_view);

            if (fragmentById != null)
            {
                SupportFragmentManager.BeginTransaction().Remove(fragmentById).Commit();
            }
        }

        var listFragment = new MyListFragment();
        SupportFragmentManager.BeginTransaction()
            .Replace(Resource.Id.fragment_container_view, listFragment)
            .Commit();
    }
    public void OnRssItemSelected(string text)
    {
        if (Resources.GetBoolean(Resource.Boolean.twoPaneMode))
        {
            var fragment = (DetailFragment?)SupportFragmentManager.FindFragmentById(Resource.Id.detailFragment);
            fragment?.SetText(text);
        }
        else
        {
            // Replace the fragment
            // Create fragment and give it an argument for the selected article
            var args = new Bundle();
            args.PutString(DetailFragment.ExtraText, text);
            var newFragment = new DetailFragment
            {
                Arguments = args
            };
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragment_container_view, newFragment)
                .SetCustomAnimations(Resource.Animator.slide_up, Resource.Animator.slide_down)
                .AddToBackStack(null)
                .Commit();
        }
    }
}