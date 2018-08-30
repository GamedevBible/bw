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
using System.Text.RegularExpressions;
using Android.Text;
using Android.Runtime;

namespace bw
{
    [Activity(Theme = "@style/AppTheme.Main", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait,
        Icon = "@drawable/Icon")]
    public class MainActivity : AppCompatActivity
    {
        private int _gameActivityCode = 777;

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
        private TextView _guessedWords;
        private TextView _guessedHardWords;

        private PreferencesHelper _preferencesHelper;
        private bool _needShowWhatsNew;
        private bool _firstStarted;
        private Locales _currentLocale;

        private const int _contactsActivityCode = 14;
        private bool _inactive;
        private EditText _editText;
        private string _gameCurrentWord;
        private bool _wordWasGuessed;
        private int _selectedCategory;

        private string[] _categories => new string[] {
            Resources.GetString(Resource.String.CatAll).ToUpper(),
            Resources.GetString(Resource.String.CatNames).ToUpper(),
            Resources.GetString(Resource.String.CatCities).ToUpper(),
            Resources.GetString(Resource.String.CatCountries).ToUpper(),
            Resources.GetString(Resource.String.CatBooks).ToUpper()};

        private string[] _levels => new string[] {
            Resources.GetString(Resource.String.LevelAll).ToUpper(),
            Resources.GetString(Resource.String.LevelEasy).ToUpper(),
            Resources.GetString(Resource.String.LevelHard).ToUpper()};

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
                CopyDatabase("");
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
                CopyDatabase("");
            }

            ApplyCulture();
            SetContentView(Resource.Layout.main);
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            InitViews();
        }

        private void CopyDatabase(string dataBaseName)
        {
            dataBaseName = "bwords.db";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + dataBaseName;

            if (System.IO.File.Exists(dbPath))
                System.IO.File.Delete(dbPath);

            if (!System.IO.File.Exists(dbPath))
            {
                var dbAssetStream = Assets.Open(dataBaseName);
                var dbFileStream = new System.IO.FileStream(dbPath, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
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
                    new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetItems(_categories, CategoryClickHandler)
                    .SetTitle(Resources.GetString(Resource.String.SelectCategory))
                        .Show();
                    break;
                case Resource.Id.recordsButton:
                    var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.FriendWordDialog)
                    .SetMessage(Resources.GetString(Resource.String.FriendWordTitle))
                    .SetPositiveButton(Resources.GetString(Resource.String.OkButton), StartFriendGame)
                    .SetNegativeButton(Resources.GetString(Resource.String.CancelButton), CloseDialog)
                    .SetCancelable(false);

                    _editText = new EditText(this);
                    _editText.SetAllCaps(true);
                    _editText.TextSize = 20;
                    _editText.SetEms(14);
                    _editText.SetMaxEms(14);
                    _editText.TextAlignment = TextAlignment.Center;
                    var maxLength = 10;
                    _editText.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(maxLength) });
                    _editText.InputType = InputTypes.ClassText;
                    dialog.SetView(_editText);
                    dialog.Create();

                    dialog.Show();
                    _editText.RequestFocus();
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

        private void CategoryClickHandler(object sender, DialogClickEventArgs e)
        {
            _inactive = false;

            _selectedCategory = e.Which;

            new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetItems(_levels, LevelClickHandler)
                    .SetTitle(Resources.GetString(Resource.String.SelectLevel))
                        .Show();
        }

        private void LevelClickHandler(object sender, DialogClickEventArgs e)
        {
            var intent = GameActivity.CreateStartIntent(this, false, string.Empty, _selectedCategory, e.Which);
            
            StartActivityForResult(intent, _gameActivityCode);
        }

        private void StartFriendGame(object sender, DialogClickEventArgs e)
        {
            var currentLanguage = Locale.Default.Language;
            _currentLocale = currentLanguage == "es" ? Locales.Spain : currentLanguage == "ru" ? Locales.Russian : Locales.English;

            var regex = _currentLocale == Locales.Russian ? @"^[а-яА-Я]+$" : @"^[a-zA-Z]+$";

            if (Regex.IsMatch(_editText.Text, regex))
            {
                var intent = GameActivity.CreateStartIntent(this, true, _editText.Text);
                StartActivityForResult(intent, _gameActivityCode);
            }
            else
            {
                Toast.MakeText(this, Resources.GetString(Resource.String.WrongFormat), ToastLength.Short).Show();
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
            _guessedWords = FindViewById<TextView>(Resource.Id.guessedWords);
            _guessedHardWords = FindViewById<TextView>(Resource.Id.guessedHardWords);
            _startButton.Click += OnButtonClicked;
            _friendButton.Click += OnButtonClicked;
            _moreGamesButton.Click += OnButtonClicked;
            _contactsButton.Click += OnButtonClicked;

            _startButton.Text = Resources.GetString(Resource.String.MenuStartGame);
            _friendButton.Text = Resources.GetString(Resource.String.MenuFriendGame);
            _moreGamesButton.Text = Resources.GetString(Resource.String.MenuOtherGames);
            _contactsButton.Text = Resources.GetString(Resource.String.MenuOptions);
            
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

        protected override void OnResume()
        {
            base.OnResume();

            _inactive = false;

            _guessedWords.Text = Resources.GetString(Resource.String.SuccessfulGames) + ": " + _preferencesHelper?.GetGuessedWords();
            _guessedHardWords.Text = Resources.GetString(Resource.String.SuccessfulHardGames) + ": " + _preferencesHelper?.GetGuessedHardWords();

            if (!string.IsNullOrEmpty(_gameCurrentWord))
            {
                if (!_wordWasGuessed)
                {
                    var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                        .SetTitle(_gameCurrentWord)
                        .SetMessage(string.Format(Resources.GetString(Resource.String.UnguessedWordText), _gameCurrentWord))
                        .SetPositiveButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                        .SetCancelable(false)
                        .Create();

                    dialog.Show();
                }
                else
                {
                    var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                        .SetTitle(_gameCurrentWord)
                        .SetMessage($"{Resources.GetString(Resource.String.CorrectToastText)} {_gameCurrentWord}!")
                        .SetPositiveButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                        .SetCancelable(false)
                        .Create();

                    dialog.Show();
                }

                _gameCurrentWord = string.Empty;
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            _inactive = false;

            if (resultCode == Result.Ok)
            {
                if (requestCode == _gameActivityCode)
                {
                    _gameCurrentWord = data.GetStringExtra("currentWord");
                    _wordWasGuessed = data.GetBooleanExtra("wordWasGuessed", false);
                }
            }
        }
    }
}

