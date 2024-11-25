using Android.Content;
using Android.Views;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace TryFragment.Fragments;

internal class MyListFragment : Fragment
{
    private IOnItemSelectedListener _listener;

    public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var view = inflater.Inflate(Resource.Layout.fragment_rsslist_overview, container, false);
        var button = view?.FindViewById<Button>(Resource.Id.updateButton);
        if (button != null)
        {
            button.Click += (sender, e) =>
            {
                UpdateDetail("testing");
            };
        }

        return view;
    }

    public interface IOnItemSelectedListener
    {
        void OnRssItemSelected(string text);
    }

    public override void OnAttach(Context context)
    {
        base.OnAttach(context);
        if (context is IOnItemSelectedListener listener)
        {
            _listener = listener;
        }
        else
        {
            throw new InvalidCastException(context.ToString() + $" must implement {nameof(MyListFragment)}.{nameof(IOnItemSelectedListener)}");
        }
    }

    public void UpdateDetail(string uri)
    {
        var newTime = $"{DateTime.Now.Ticks} {uri}";
        _listener.OnRssItemSelected(newTime);
    }
}
