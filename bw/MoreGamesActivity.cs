using Android.App;
using Android.Support.V7.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System;
using Microsoft.AppCenter.Analytics;
using Android;
using Android.Content.PM;
using Java.Util;

namespace bw
{
    [Activity(Label = "MoreGamesActivity", Theme = "@style/AppTheme.Main", ScreenOrientation = ScreenOrientation.Portrait,
        Icon = "@mipmap/ic_launcher")]
    public class MoreGamesActivity : AppCompatActivity
    {
        private TextView _fpowTextView;
        private TextView _fpowTitleTextView;
        private Button _fpowButton;

        private TextView _bmTextView;
        private TextView _bmTitleTextView;
        private Button _bmButton;

        private const string _fpowPackageName = "com.biblegamedev.fpow";
        private const string _bmPackageName = "com.BM.Droid";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.more_games);

            _fpowTextView = FindViewById<TextView>(Resource.Id.fpowtext);
            _fpowTitleTextView = FindViewById<TextView>(Resource.Id.fpowtitletext);
            _fpowButton = FindViewById<Button>(Resource.Id.fpowButton);
            _bmTextView = FindViewById<TextView>(Resource.Id.bmtext);
            _bmTitleTextView = FindViewById<TextView>(Resource.Id.bmtitletext);
            _bmButton = FindViewById<Button>(Resource.Id.bmButton);
            _bmButton.Click += OnButtonClicked;
            _fpowButton.Click += OnButtonClicked;

            _fpowTitleTextView.Text = Locale.Default.Language == "ru" ? "4 ���� 1 ����� - ������" : "4 photos 1 word - Bible";
            _fpowTextView.Text = "����, � ������� ��� ��������� ��������, ����� ����� ������ � �������������� ������� �����������." + "\n" +
                "���������� �������� ��� ���������� �����!" + "\n" +
                "� ���� ����� ���������� ����� ����������� ����� ������!";
            _fpowTextView.Visibility = Locale.Default.Language == "ru" ? Android.Views.ViewStates.Visible : Android.Views.ViewStates.Gone;

            _bmTitleTextView.Text = Locale.Default.Language == "ru" ? "��������� - ������ (������� ����)" : "Millionaire - Bible (only russian language)";
            _bmTextView.Text = "����, � ������� ��� ��������� ������� ���� �� ������� �������������� ������� �� ������� �� ������." + "\n" +
                "���������� ����� �� ������ ����� � �������� ������� �����!" + "\n" +
                "� ���� ��� ���� ����� 3000 ��������!";
            _bmTextView.Visibility = Locale.Default.Language == "ru" ? Android.Views.ViewStates.Visible : Android.Views.ViewStates.Gone;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            switch (btn.Id)
            {
                case Resource.Id.fpowButton:
                    Analytics.TrackEvent("User go to FPOW from BW");
                    try
                    {
                        StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + _fpowPackageName)));
                    }
                    catch (ActivityNotFoundException anfe)
                    {
                        StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + _fpowPackageName)));
                    }
                    break;

                case Resource.Id.bmButton:
                    Analytics.TrackEvent("User go to BM from BW");
                    try
                    {
                        StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + _bmPackageName)));
                    }
                    catch (ActivityNotFoundException anfe)
                    {
                        StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + _bmPackageName)));
                    }
                    break;
            }
        }

        public static Intent CreateStartIntent(Context context, string message = null)
        {
            var intent = new Intent(context, typeof(MoreGamesActivity));

            return intent;
        }
    }
}