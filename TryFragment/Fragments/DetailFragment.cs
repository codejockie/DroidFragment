using Android.Views;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace TryFragment.Fragments;

internal class DetailFragment : Fragment
{
    public const string ExtraText = "text";

    public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var view = inflater.Inflate(Resource.Layout.fragment_rssitem_detail, container, false);
        return view;
    }

    public override void OnViewCreated(View view, Bundle? savedInstanceState)
    {
        base.OnViewCreated(view, savedInstanceState);
        var bundle = Arguments;

        if (bundle != null)
        {
            var text = bundle.GetString(ExtraText);
            SetText(text);
        }
    }

    public void SetText(string? text)
    {
        var view = View?.FindViewById<TextView>(Resource.Id.detailsText);
        if (view != null) view.Text = text;
    }
}
