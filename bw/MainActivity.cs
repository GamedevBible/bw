using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Java.Util;
using System.Collections.Generic;
using System.Linq;
using Android.Content.Res;
using Android.Content.PM;
using Android.Views;
using Android.Content;
using Android.Widget;
using System;

namespace bw
{
    [Activity(Theme = "@style/AppTheme.Main", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait,
        Icon = "@drawable/Icon")]
    public class MainActivity : AppCompatActivity
    {
        public enum Locales
        {
            English,
            Russian,
            Spain
        }

        private Button _startButton;
        private Button _friendButton;
        private Button _contactsButton;
        private Button _moreGamesButton;

        private PreferencesHelper _preferencesHelper;
        private bool _needShowWhatsNew;
        private bool _firstStarted;
        private Locales _currentLocale;

        private const int _contactsActivityCode = 14;
        private bool _inactive;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _preferencesHelper = new PreferencesHelper();
            _preferencesHelper.InitHepler(this);
            _firstStarted = _preferencesHelper.GetFirstStarted();
            
            _needShowWhatsNew = BwConfig.NeedShowWhatsNew;

            if (_firstStarted)
            {
                ShowGreetingsAlert();
                _firstStarted = false;
                _preferencesHelper.PutFirstStarted(this, _firstStarted);
                _preferencesHelper.PutLastVersion(this, PackageManager.GetPackageInfo(PackageName, PackageInfoFlags.Configurations).VersionName);
            }
            else
            if (!_preferencesHelper.GetLastVersion().Equals(PackageManager.GetPackageInfo(PackageName, PackageInfoFlags.Configurations).VersionName))
            {
                if (_needShowWhatsNew)
                    ShowWhatsNewAlert();
                _preferencesHelper.PutLastVersion(this, PackageManager.GetPackageInfo(PackageName, PackageInfoFlags.Configurations).VersionName);
            }

            ApplyCulture();
            SetContentView(Resource.Layout.main);
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            InitViews();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (_inactive)
                return;
            _inactive = true;

            var clickedButton = (Button)sender;

            switch (clickedButton.Id)
            {
                case Resource.Id.startButton:
                    _inactive = false;
                    StartActivity(GameActivity.CreateStartIntent(this));
                    break;
                case Resource.Id.recordsButton:
                    var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.FriendWordDialog)
                    .SetMessage("Загадайте слово, нажмите ОК, и передайте устройство другу.")
                    .SetPositiveButton(Resources.GetString(Resource.String.OkButton), CloseDialog)
                    .SetNegativeButton(Resources.GetString(Resource.String.CancelButton), CloseDialog)
                    .SetCancelable(false)
                    .Create();

                    dialog.Show();
                    _inactive = false;
                    break;
                case Resource.Id.guideButton:
                    StartActivity(MoreGamesActivity.CreateStartIntent(this));
                    _inactive = false;
                    break;
                case Resource.Id.contactsButton:
                    StartActivityForResult(ContactsActivity.CreateStartIntent(this), _contactsActivityCode);
                    _inactive = false;
                    break;
                default:
                    break;
            }
        }

        private void ShowWhatsNewAlert()
        {
            var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                    .SetTitle($"{Resources.GetString(Resource.String.VersionTitle)} {PackageManager.GetPackageInfo(PackageName, PackageInfoFlags.Configurations).VersionName}")
                    .SetMessage(Resources.GetString(Resource.String.WhatsNewTitle) + "\n"
                    + (_currentLocale == Locales.English ? BwConfig.WhatsNewEnglishMessage : _currentLocale == Locales.Russian
                        ? BwConfig.WhatsNewRussianMessage
                        : BwConfig.WhatsNewSpanishMessage))
                    .SetPositiveButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                    .SetCancelable(false)
                    .Create();

            dialog.Show();
        }

        private void ShowGreetingsAlert()
        {
            var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                    .SetTitle(Resources.GetString(Resource.String.GreetingsTitle))
                    .SetMessage(Resources.GetString(Resource.String.GreetingsMessage))
                    .SetPositiveButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                    .SetCancelable(false)
                    .Create();

            dialog.Show();
        }

        private void InitViews()
        {
            _startButton = FindViewById<Button>(Resource.Id.startButton);
            _friendButton = FindViewById<Button>(Resource.Id.recordsButton);
            _moreGamesButton = FindViewById<Button>(Resource.Id.guideButton);
            _contactsButton = FindViewById<Button>(Resource.Id.contactsButton);
            _startButton.Click += OnButtonClicked;
            _friendButton.Click += OnButtonClicked;
            _moreGamesButton.Click += OnButtonClicked;
            _contactsButton.Click += OnButtonClicked;

            var currentLanguage = Locale.Default.Language;
            _currentLocale = currentLanguage == "es" ? Locales.Spain : currentLanguage == "ru" ? Locales.Russian : Locales.English;
        }

        private void CloseDialog(object sender, DialogClickEventArgs e)
        {
            ((Android.Support.V7.App.AlertDialog)sender).Dismiss();
        }

        private void ApplyCulture()
        {
            var currentLocale = Locale.Default;
            var selectedLocaleIndex = _preferencesHelper.GetSelectedLanguage();

            if (selectedLocaleIndex != 0)
            {
                switch (selectedLocaleIndex)
                {
                    case 1:
                        var enLocale = new Locale("en");
                        if (currentLocale.Language != enLocale.Language)
                        {
                            Locale.Default = enLocale;
                            var config = new Configuration { Locale = enLocale };
                            BaseContext.Resources.UpdateConfiguration(config, BaseContext.Resources.DisplayMetrics);
                        }
                        break;
                    case 2:
                        var ruLocale = new Locale("ru");
                        if (currentLocale.Language != ruLocale.Language)
                        {
                            Locale.Default = ruLocale;
                            var config1 = new Configuration { Locale = ruLocale };
                            BaseContext.Resources.UpdateConfiguration(config1, BaseContext.Resources.DisplayMetrics);
                        }
                        break;
                    case 3:
                        var esLocale = new Locale("es");
                        if (currentLocale.Language != esLocale.Language)
                        {
                            Locale.Default = esLocale;
                            var config2 = new Configuration { Locale = esLocale };
                            BaseContext.Resources.UpdateConfiguration(config2, BaseContext.Resources.DisplayMetrics);
                        }
                        break;
                }

                return;
            }

            // Set default culture by phone culture
            var listOfRussianLocales = new List<Locale>
            {
                new Locale("ru"),
                new Locale("be"),
                new Locale("uk"),
                new Locale("az"),
                new Locale("hy"),
                new Locale("kk"),
                new Locale("ky"),
                new Locale("tt"),
                new Locale("uz")
            };

            if (listOfRussianLocales.Any(t => t.Language == currentLocale.Language))
            {
                var newLocale = new Locale("ru");

                Locale.Default = newLocale;
                var config = new Configuration { Locale = newLocale };
                BaseContext.Resources.UpdateConfiguration(config, BaseContext.Resources.DisplayMetrics);
            }

            var spanishLocale = new Locale("es");

            if (currentLocale.Language == spanishLocale.Language)
            {
                Locale.Default = spanishLocale;
                var config = new Configuration { Locale = spanishLocale };
                BaseContext.Resources.UpdateConfiguration(config, BaseContext.Resources.DisplayMetrics);
            }
        }
    }
}

