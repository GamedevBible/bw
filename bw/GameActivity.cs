using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Java.Util;
using Android.Content.PM;
using Android.Views.Animations;
using Android.Support.V7.Widget;
using System.Threading.Tasks;
using Com.Bumptech.Glide;
using Android.Graphics.Drawables;
using Com.Bumptech.Glide.Load.Engine;

namespace bw
{
    [Activity(Label = "GameActivity", Theme = "@style/AppTheme.Main", ScreenOrientation = ScreenOrientation.Portrait,
        Icon = "@mipmap/ic_launcher")]
    internal class GameActivity : AppCompatActivity
    {
        private View _letter1Layout;
        private Button _letter1Button;
        private View _letter2Layout;
        private Button _letter2Button;
        private View _letter3Layout;
        private Button _letter3Button;
        private View _letter4Layout;
        private Button _letter4Button;
        private View _letter5Layout;
        private Button _letter5Button;
        private View _letter6Layout;
        private Button _letter6Button;
        private View _letter7Layout;
        private Button _letter7Button;
        private View _letter8Layout;
        private Button _letter8Button;
        private View _letter9Layout;
        private Button _letter9Button;
        private View _letter10Layout;
        private Button _letter10Button;
        private View _letter11Layout;
        private Button _letter11Button;
        private View _letter12Layout;
        private Button _letter12Button;
        private View _letter13Layout;
        private Button _letter13Button;
        private View _letter14Layout;
        private Button _letter14Button;
        private View _letter15Layout;
        private Button _letter15Button;
        private View _letter16Layout;
        private Button _letter16Button;
        private View _letter17Layout;
        private Button _letter17Button;
        private View _letter18Layout;
        private Button _letter18Button;
        private View _letter19Layout;
        private Button _letter19Button;
        private View _letter20Layout;
        private Button _letter20Button;
        private View _letter21Layout;
        private Button _letter21Button;
        private View _letter22Layout;
        private Button _letter22Button;
        private View _letter23Layout;
        private Button _letter23Button;
        private View _letter24Layout;
        private Button _letter24Button;
        private View _letter25Layout;
        private Button _letter25Button;
        private View _letter26Layout;
        private Button _letter26Button;
        private View _letter27Layout;
        private Button _letter27Button;
        private View _letter28Layout;
        private Button _letter28Button;
        private View _letter29Layout;
        private Button _letter29Button;
        private View _letter30Layout;
        private Button _letter30Button;
        private View _letter31Layout;
        private Button _letter31Button;
        private View _letter32Layout;
        private Button _letter32Button;
        private View _letter33Layout;
        private Button _letter33Button;
        private View _letter34Layout;
        private Button _letter34Button;
        private View _letter35Layout;
        private Button _letter35Button;
        private View _letter36Layout;
        private Button _letter36Button;
        private View _letter37Layout;
        private Button _letter37Button;
        private View _letter38Layout;
        private Button _letter38Button;
        private View _letter39Layout;
        private Button _letter39Button;
        private View _letter40Layout;
        private Button _letter40Button;

        private View _word1layout;
        private Button _word1button;
        private View _word2layout;
        private Button _word2button;
        private View _word3layout;
        private Button _word3button;
        private View _word4layout;
        private Button _word4button;
        private View _word5layout;
        private Button _word5button;
        private View _word6layout;
        private Button _word6button;
        private View _word7layout;
        private Button _word7button;
        private View _word8layout;
        private Button _word8button;
        private View _word9layout;
        private Button _word9button;
        private View _word10layout;
        private Button _word10button;

        private AppCompatImageButton _backButton;
        private AppCompatImageButton _hintButton;

        private Android.Support.V7.App.AlertDialog _myDialog;
        private bool _inactive;
        private bool _friendMode;
        private string _category;
        private int _categoryIndex;
        private int _levelIndex;
        private TextView _categoryTV;
        private bool _needFinishActivity;
        private bool _hintWasClicked;
        private string _currentWord;
        private int _currentWordId;
        private string _level;
        private int[] _alphabetIntArray;
        private List<int> _exceptIds = new List<int>();
        private string[] _alphabetArray;
        private ProgressDialog _progressDialog;
        private Locales _currentLocale;
        private int _lifes;
        private ImageView _bridgeImage;

        private PreferencesHelper _preferencesHelper;
        private WordsDatabase _wordsDatabase;

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
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            
            _friendMode = Intent.GetBooleanExtra(nameof(_friendMode), false);

            _preferencesHelper = new PreferencesHelper();
            _preferencesHelper.InitHepler(this);

            _progressDialog = new ProgressDialog(this, Resource.Style.ProgressDialogTheme) { Indeterminate = true };
            _progressDialog.SetCancelable(false);
            _progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            _progressDialog.SetMessage(Resources.GetString(Resource.String.Loading));

            if (_friendMode)
            {
                _currentWord = Intent.GetStringExtra(nameof(_currentWord)) ?? string.Empty;
                _category = Resources.GetString(Resource.String.FriendsWord).ToUpper();
            }
            else
            {
                if (!_progressDialog.IsShowing)
                    _progressDialog.Show();

                if (_wordsDatabase == null)
                    _wordsDatabase = new WordsDatabase(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
                                
                _categoryIndex = Intent.GetIntExtra(nameof(_categoryIndex), 0);
                _levelIndex = Intent.GetIntExtra(nameof(_levelIndex), 0);
                _category = _categories[_categoryIndex];
                _level = _levels[_levelIndex];

                var wordInfo = await Task.Run(() => _wordsDatabase.GetItem(_categoryIndex, _levelIndex, new int[] { }));
                _currentWord = wordInfo.Word;
                _currentWordId = wordInfo.Id;

                if (string.IsNullOrWhiteSpace(_currentWord) && _currentWordId == -1)
                {
                    if (_progressDialog.IsShowing)
                        _progressDialog.Dismiss();
                    ShowAllWordsGuessedAlert();
                    return;
                }

                if (_progressDialog.IsShowing)
                    _progressDialog.Dismiss();
            }

            var currentLanguage = Locale.Default.Language;
            _currentLocale = currentLanguage == "es" ? Locales.Spain : currentLanguage == "ru" ? Locales.Russian : Locales.English;

            _alphabetIntArray = new int[40];
            _alphabetArray = new string[40];
            _lifes = 7;

            for (int i = 0; i < 40; i++)
            {
                if (i < GameHelper.GetAlphabet(_currentLocale).Length)
                {
                    _alphabetIntArray[i] = 1;
                    _alphabetArray[i] = GameHelper.GetAlphabet(_currentLocale)[i];
                }  
                else
                {
                    _alphabetIntArray[i] = 2;
                    _alphabetArray[i] = string.Empty;
                } 
            }

            if (savedInstanceState != null)
            {
                _currentWord = savedInstanceState.GetString(nameof(_currentWord));
                _alphabetIntArray = savedInstanceState.GetIntArray(nameof(_alphabetIntArray));
                _lifes = savedInstanceState.GetInt(nameof(_lifes));
                _categoryIndex = savedInstanceState.GetInt(nameof(_categoryIndex));
                _needFinishActivity = savedInstanceState.GetBoolean(nameof(_needFinishActivity));
                _hintWasClicked = savedInstanceState.GetBoolean(nameof(_hintWasClicked));
                _friendMode = savedInstanceState.GetBoolean(nameof(_friendMode));
                _levelIndex = savedInstanceState.GetInt(nameof(_levelIndex));
                _exceptIds = savedInstanceState.GetIntArray(nameof(_exceptIds)).ToList();
                _category = _categories[_categoryIndex];
                _level = _levels[_levelIndex];
                _currentWordId = savedInstanceState.GetInt(nameof(_currentWordId));
            }

            SetContentView(Resource.Layout.game);

            InitViews();

            InitGameAndStart();
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (_needFinishActivity)
            {
                Intent myIntent = new Intent(this, typeof(MainActivity));
                if (!string.IsNullOrEmpty(_currentWord))
                {
                    myIntent.PutExtra("currentWord", _currentWord.ToUpper());
                    myIntent.PutExtra("wordWasGuessed", false);
                }
                SetResult(Result.Ok, myIntent);
                Finish();
            }
        }

        private void InitGameAndStart()
        {
            ApplyWordVisibility();
            ApplyLettersVisibility();
            FillAlphabet();
            RefreshAlphabet();
        }

        private void OpenLetter(string letter)
        {
            for (var i = 0; i < _currentWord.Length; i++)
            {
                if (_currentWord[i].ToString().ToUpper() == letter.ToUpper())
                {
                    switch(i)
                    {
                        case 0:
                            _word1button.Text = letter.ToUpper();
                            break;
                        case 1:
                            _word2button.Text = letter.ToUpper();
                            break;
                        case 2:
                            _word3button.Text = letter.ToUpper();
                            break;
                        case 3:
                            _word4button.Text = letter.ToUpper();
                            break;
                        case 4:
                            _word5button.Text = letter.ToUpper();
                            break;
                        case 5:
                            _word6button.Text = letter.ToUpper();
                            break;
                        case 6:
                            _word7button.Text = letter.ToUpper();
                            break;
                        case 7:
                            _word8button.Text = letter.ToUpper();
                            break;
                        case 8:
                            _word9button.Text = letter.ToUpper();
                            break;
                        case 9:
                            _word10button.Text = letter.ToUpper();
                            break;
                    }
                }
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            if (_progressDialog.IsShowing)
                _progressDialog.Dismiss();

            if (_currentWord == null)
                _currentWord = string.Empty;

            if (_alphabetIntArray == null)
            {
                _alphabetIntArray = new int[GameHelper.GetAlphabet(_currentLocale).Length];
                for (int i = 0; i < GameHelper.GetAlphabet(_currentLocale).Length; i++)
                {
                    _alphabetIntArray[i] = 1;
                }
            }
            
            outState.PutString(nameof(_currentWord), _currentWord);
            outState.PutIntArray(nameof(_alphabetIntArray), _alphabetIntArray);
            outState.PutInt(nameof(_lifes), _lifes);
            outState.PutInt(nameof(_categoryIndex), _categoryIndex);
            outState.PutInt(nameof(_levelIndex), _levelIndex);
            outState.PutBoolean(nameof(_needFinishActivity), _needFinishActivity);
            outState.PutBoolean(nameof(_hintWasClicked), _hintWasClicked);
            outState.PutBoolean(nameof(_friendMode), _friendMode);
            outState.PutIntArray(nameof(_exceptIds), _exceptIds.ToArray());
            outState.PutInt(nameof(_currentWordId), _currentWordId);
        }

        private void RefreshAlphabet()
        {
            _letter1Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter1Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter2Layout.Enabled = _alphabetIntArray[1] == 1;
            _letter2Button.SetBackgroundResource(_alphabetIntArray[1] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter3Layout.Enabled = _alphabetIntArray[2] == 1;
            _letter3Button.SetBackgroundResource(_alphabetIntArray[2] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter4Layout.Enabled = _alphabetIntArray[3] == 1;
            _letter4Button.SetBackgroundResource(_alphabetIntArray[3] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter5Layout.Enabled = _alphabetIntArray[4] == 1;
            _letter5Button.SetBackgroundResource(_alphabetIntArray[4] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter6Layout.Enabled = _alphabetIntArray[5] == 1;
            _letter6Button.SetBackgroundResource(_alphabetIntArray[5] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter7Layout.Enabled = _alphabetIntArray[6] == 1;
            _letter7Button.SetBackgroundResource(_alphabetIntArray[6] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter8Layout.Enabled = _alphabetIntArray[7] == 1;
            _letter8Button.SetBackgroundResource(_alphabetIntArray[7] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter9Layout.Enabled = _alphabetIntArray[8] == 1;
            _letter9Button.SetBackgroundResource(_alphabetIntArray[8] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter10Layout.Enabled = _alphabetIntArray[9] == 1;
            _letter10Button.SetBackgroundResource(_alphabetIntArray[9] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter11Layout.Enabled = _alphabetIntArray[10] == 1;
            _letter11Button.SetBackgroundResource(_alphabetIntArray[10] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter12Layout.Enabled = _alphabetIntArray[11] == 1;
            _letter12Button.SetBackgroundResource(_alphabetIntArray[11] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter13Layout.Enabled = _alphabetIntArray[12] == 1;
            _letter13Button.SetBackgroundResource(_alphabetIntArray[12] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter14Layout.Enabled = _alphabetIntArray[13] == 1;
            _letter14Button.SetBackgroundResource(_alphabetIntArray[13] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter15Layout.Enabled = _alphabetIntArray[14] == 1;
            _letter15Button.SetBackgroundResource(_alphabetIntArray[14] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter16Layout.Enabled = _alphabetIntArray[15] == 1;
            _letter16Button.SetBackgroundResource(_alphabetIntArray[15] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter17Layout.Enabled = _alphabetIntArray[16] == 1;
            _letter17Button.SetBackgroundResource(_alphabetIntArray[16] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter18Layout.Enabled = _alphabetIntArray[17] == 1;
            _letter18Button.SetBackgroundResource(_alphabetIntArray[17] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter19Layout.Enabled = _alphabetIntArray[18] == 1;
            _letter19Button.SetBackgroundResource(_alphabetIntArray[18] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter20Layout.Enabled = _alphabetIntArray[19] == 1;
            _letter20Button.SetBackgroundResource(_alphabetIntArray[19] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter21Layout.Enabled = _alphabetIntArray[20] == 1;
            _letter21Button.SetBackgroundResource(_alphabetIntArray[20] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter22Layout.Enabled = _alphabetIntArray[21] == 1;
            _letter22Button.SetBackgroundResource(_alphabetIntArray[21] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter23Layout.Enabled = _alphabetIntArray[22] == 1;
            _letter23Button.SetBackgroundResource(_alphabetIntArray[22] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter24Layout.Enabled = _alphabetIntArray[23] == 1;
            _letter24Button.SetBackgroundResource(_alphabetIntArray[23] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter25Layout.Enabled = _alphabetIntArray[24] == 1;
            _letter25Button.SetBackgroundResource(_alphabetIntArray[24] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter26Layout.Enabled = _alphabetIntArray[25] == 1;
            _letter26Button.SetBackgroundResource(_alphabetIntArray[25] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter27Layout.Enabled = _alphabetIntArray[26] == 1;
            _letter27Button.SetBackgroundResource(_alphabetIntArray[26] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter28Layout.Enabled = _alphabetIntArray[27] == 1;
            _letter28Button.SetBackgroundResource(_alphabetIntArray[27] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter29Layout.Enabled = _alphabetIntArray[28] == 1;
            _letter29Button.SetBackgroundResource(_alphabetIntArray[28] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter30Layout.Enabled = _alphabetIntArray[29] == 1;
            _letter30Button.SetBackgroundResource(_alphabetIntArray[29] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter31Layout.Enabled = _alphabetIntArray[30] == 1;
            _letter31Button.SetBackgroundResource(_alphabetIntArray[30] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter32Layout.Enabled = _alphabetIntArray[31] == 1;
            _letter32Button.SetBackgroundResource(_alphabetIntArray[31] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter33Layout.Enabled = _alphabetIntArray[32] == 1;
            _letter33Button.SetBackgroundResource(_alphabetIntArray[32] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter34Layout.Enabled = _alphabetIntArray[33] == 1;
            _letter34Button.SetBackgroundResource(_alphabetIntArray[33] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter35Layout.Enabled = _alphabetIntArray[34] == 1;
            _letter35Button.SetBackgroundResource(_alphabetIntArray[34] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter36Layout.Enabled = _alphabetIntArray[35] == 1;
            _letter36Button.SetBackgroundResource(_alphabetIntArray[35] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter37Layout.Enabled = _alphabetIntArray[36] == 1;
            _letter37Button.SetBackgroundResource(_alphabetIntArray[36] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter38Layout.Enabled = _alphabetIntArray[37] == 1;
            _letter38Button.SetBackgroundResource(_alphabetIntArray[37] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter39Layout.Enabled = _alphabetIntArray[38] == 1;
            _letter39Button.SetBackgroundResource(_alphabetIntArray[38] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter40Layout.Enabled = _alphabetIntArray[39] == 1;
            _letter40Button.SetBackgroundResource(_alphabetIntArray[39] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);

            for (var i = 0; i < _alphabetIntArray.Length; i++)
            {
                if (_alphabetIntArray[i] == 0)
                    OpenLetter(_alphabetArray[i]);
            }

            var allWordFilled = AllWordFilled();
            if (allWordFilled)
            {
                CheckWordGuessed();
            }
        }

        private void CheckWordGuessed()
        {
            if(_friendMode)
            {
                Intent myIntent = new Intent(this, typeof(MainActivity));
                myIntent.PutExtra("currentWord", _currentWord.ToUpper());
                myIntent.PutExtra("wordWasGuessed", true);
                SetResult(Result.Ok, myIntent);
                Finish();
            }
            else
            {
                _exceptIds.Add(_currentWordId);
                ShowFinishAlert(true);
            }
        }

        private bool AllWordFilled()
        {
            if (_word1layout.Visibility == ViewStates.Visible)
            {
                if (!string.IsNullOrEmpty(_word1button.Text))
                {
                    if (_word2layout.Visibility == ViewStates.Visible)
                    {
                        if (!string.IsNullOrEmpty(_word2button.Text))
                        {
                            if (_word3layout.Visibility == ViewStates.Visible)
                            {
                                if (!string.IsNullOrEmpty(_word3button.Text))
                                {
                                    if (_word4layout.Visibility == ViewStates.Visible)
                                    {
                                        if (!string.IsNullOrEmpty(_word4button.Text))
                                        {
                                            if (_word5layout.Visibility == ViewStates.Visible)
                                            {
                                                if (!string.IsNullOrEmpty(_word5button.Text))
                                                {
                                                    if (_word6layout.Visibility == ViewStates.Visible)
                                                    {
                                                        if (!string.IsNullOrEmpty(_word6button.Text))
                                                        {
                                                            if (_word7layout.Visibility == ViewStates.Visible)
                                                            {
                                                                if (!string.IsNullOrEmpty(_word7button.Text))
                                                                {
                                                                    if (_word8layout.Visibility == ViewStates.Visible)
                                                                    {
                                                                        if (!string.IsNullOrEmpty(_word8button.Text))
                                                                        {
                                                                            if (_word9layout.Visibility == ViewStates.Visible)
                                                                            {
                                                                                if (!string.IsNullOrEmpty(_word9button.Text))
                                                                                {
                                                                                    if (_word10layout.Visibility == ViewStates.Visible)
                                                                                    {
                                                                                        if (!string.IsNullOrEmpty(_word10button.Text))
                                                                                        {
                                                                                            return true;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        return true;
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                return true;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        private void ApplyWordVisibility()
        {
            switch (_currentWord.Length)
            {
                case 1:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Gone;
                    _word3layout.Visibility = ViewStates.Gone;
                    _word4layout.Visibility = ViewStates.Gone;
                    _word5layout.Visibility = ViewStates.Gone;
                    _word6layout.Visibility = ViewStates.Gone;
                    _word7layout.Visibility = ViewStates.Gone;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 2:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Gone;
                    _word4layout.Visibility = ViewStates.Gone;
                    _word5layout.Visibility = ViewStates.Gone;
                    _word6layout.Visibility = ViewStates.Gone;
                    _word7layout.Visibility = ViewStates.Gone;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 3:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Gone;
                    _word5layout.Visibility = ViewStates.Gone;
                    _word6layout.Visibility = ViewStates.Gone;
                    _word7layout.Visibility = ViewStates.Gone;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 4:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Gone;
                    _word6layout.Visibility = ViewStates.Gone;
                    _word7layout.Visibility = ViewStates.Gone;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 5:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Visible;
                    _word6layout.Visibility = ViewStates.Gone;
                    _word7layout.Visibility = ViewStates.Gone;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 6:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Visible;
                    _word6layout.Visibility = ViewStates.Visible;
                    _word7layout.Visibility = ViewStates.Gone;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 7:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Visible;
                    _word6layout.Visibility = ViewStates.Visible;
                    _word7layout.Visibility = ViewStates.Visible;
                    _word8layout.Visibility = ViewStates.Gone;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 8:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Visible;
                    _word6layout.Visibility = ViewStates.Visible;
                    _word7layout.Visibility = ViewStates.Visible;
                    _word8layout.Visibility = ViewStates.Visible;
                    _word9layout.Visibility = ViewStates.Gone;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 9:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Visible;
                    _word6layout.Visibility = ViewStates.Visible;
                    _word7layout.Visibility = ViewStates.Visible;
                    _word8layout.Visibility = ViewStates.Visible;
                    _word9layout.Visibility = ViewStates.Visible;
                    _word10layout.Visibility = ViewStates.Gone;
                    break;
                case 10:
                    _word1layout.Visibility = ViewStates.Visible;
                    _word2layout.Visibility = ViewStates.Visible;
                    _word3layout.Visibility = ViewStates.Visible;
                    _word4layout.Visibility = ViewStates.Visible;
                    _word5layout.Visibility = ViewStates.Visible;
                    _word6layout.Visibility = ViewStates.Visible;
                    _word7layout.Visibility = ViewStates.Visible;
                    _word8layout.Visibility = ViewStates.Visible;
                    _word9layout.Visibility = ViewStates.Visible;
                    _word10layout.Visibility = ViewStates.Visible;
                    break;
            }
        }

        private void FillAlphabet()
        {
            _letter1Button.Text = _alphabetArray[0];
            _letter2Button.Text = _alphabetArray[1];
            _letter3Button.Text = _alphabetArray[2];
            _letter4Button.Text = _alphabetArray[3];
            _letter5Button.Text = _alphabetArray[4];
            _letter6Button.Text = _alphabetArray[5];
            _letter7Button.Text = _alphabetArray[6];
            _letter8Button.Text = _alphabetArray[7];
            _letter9Button.Text = _alphabetArray[8];
            _letter10Button.Text = _alphabetArray[9];
            _letter11Button.Text = _alphabetArray[10];
            _letter12Button.Text = _alphabetArray[11];
            _letter13Button.Text = _alphabetArray[12];
            _letter14Button.Text = _alphabetArray[13];
            _letter15Button.Text = _alphabetArray[14];
            _letter16Button.Text = _alphabetArray[15];
            _letter17Button.Text = _alphabetArray[16];
            _letter18Button.Text = _alphabetArray[17];
            _letter19Button.Text = _alphabetArray[18];
            _letter20Button.Text = _alphabetArray[19];
            _letter21Button.Text = _alphabetArray[20];
            _letter22Button.Text = _alphabetArray[21];
            _letter23Button.Text = _alphabetArray[22];
            _letter24Button.Text = _alphabetArray[23];
            _letter25Button.Text = _alphabetArray[24];
            _letter26Button.Text = _alphabetArray[25];
            _letter27Button.Text = _alphabetArray[26];
            _letter28Button.Text = _alphabetArray[27];
            _letter29Button.Text = _alphabetArray[28];
            _letter30Button.Text = _alphabetArray[29];
            _letter31Button.Text = _alphabetArray[30];
            _letter32Button.Text = _alphabetArray[31];
            _letter33Button.Text = _alphabetArray[32];
            _letter34Button.Text = _alphabetArray[33];
            _letter35Button.Text = _alphabetArray[34];
            _letter36Button.Text = _alphabetArray[35];
            _letter37Button.Text = _alphabetArray[36];
            _letter38Button.Text = _alphabetArray[37];
            _letter39Button.Text = _alphabetArray[38];
            _letter40Button.Text = _alphabetArray[39];
        }

        private void ApplyLettersVisibility()
        {
            _letter1Layout.Visibility = _alphabetIntArray[0] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter2Layout.Visibility = _alphabetIntArray[1] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter3Layout.Visibility = _alphabetIntArray[2] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter4Layout.Visibility = _alphabetIntArray[3] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter5Layout.Visibility = _alphabetIntArray[4] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter6Layout.Visibility = _alphabetIntArray[5] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter7Layout.Visibility = _alphabetIntArray[6] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter8Layout.Visibility = _alphabetIntArray[7] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter9Layout.Visibility = _alphabetIntArray[8] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter10Layout.Visibility = _alphabetIntArray[9] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter11Layout.Visibility = _alphabetIntArray[10] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter12Layout.Visibility = _alphabetIntArray[11] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter13Layout.Visibility = _alphabetIntArray[12] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter14Layout.Visibility = _alphabetIntArray[13] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter15Layout.Visibility = _alphabetIntArray[14] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter16Layout.Visibility = _alphabetIntArray[15] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter17Layout.Visibility = _alphabetIntArray[16] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter18Layout.Visibility = _alphabetIntArray[17] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter19Layout.Visibility = _alphabetIntArray[18] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter20Layout.Visibility = _alphabetIntArray[19] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter21Layout.Visibility = _alphabetIntArray[20] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter22Layout.Visibility = _alphabetIntArray[21] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter23Layout.Visibility = _alphabetIntArray[22] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter24Layout.Visibility = _alphabetIntArray[23] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter25Layout.Visibility = _alphabetIntArray[24] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter26Layout.Visibility = _alphabetIntArray[25] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter27Layout.Visibility = _alphabetIntArray[26] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter28Layout.Visibility = _alphabetIntArray[27] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter29Layout.Visibility = _alphabetIntArray[28] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter30Layout.Visibility = _alphabetIntArray[29] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter31Layout.Visibility = _alphabetIntArray[30] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter32Layout.Visibility = _alphabetIntArray[31] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter33Layout.Visibility = _alphabetIntArray[32] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter34Layout.Visibility = _alphabetIntArray[33] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter35Layout.Visibility = _alphabetIntArray[34] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter36Layout.Visibility = _alphabetIntArray[35] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter37Layout.Visibility = _alphabetIntArray[36] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter38Layout.Visibility = _alphabetIntArray[37] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter39Layout.Visibility = _alphabetIntArray[38] != 2 ? ViewStates.Visible : ViewStates.Gone;
            _letter40Layout.Visibility = _alphabetIntArray[39] != 2 ? ViewStates.Visible : ViewStates.Gone;
        }

        public static Intent CreateStartIntent(Context context, bool friendMode, string currentWord = "", int category = -1, int level = -1)
        {
            var intent = new Intent(context, typeof(GameActivity));
            
            intent.PutExtra(nameof(_currentWord), currentWord);
            intent.PutExtra(nameof(_categoryIndex), category);
            intent.PutExtra(nameof(_friendMode), friendMode);
            intent.PutExtra(nameof(_levelIndex), level);

            return intent;
        }

        private async void InitViews()
        {
            _bridgeImage = FindViewById<ImageView>(Resource.Id.bridgeImage);

            //await Task.Factory.StartNew(() => Glide.Get(this).ClearDiskCache());

            /*RunOnUiThread(() =>
            {
                Glide.Get(this).ClearMemory();
            });*/

            RunOnUiThread(() =>
            {
                Glide.With(this).Load(Resource.Drawable.bridge_default).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
            });
            _categoryTV = FindViewById<TextView>(Resource.Id.category);
            _categoryTV.Text = _category;

            _letter1Layout = FindViewById<View>(Resource.Id.letter1Layout);
            _letter1Button = FindViewById<Button>(Resource.Id.letter1Button);
            _letter2Layout = FindViewById<View>(Resource.Id.letter2Layout);
            _letter2Button = FindViewById<Button>(Resource.Id.letter2Button);
            _letter3Layout = FindViewById<View>(Resource.Id.letter3Layout);
            _letter3Button = FindViewById<Button>(Resource.Id.letter3Button);
            _letter4Layout = FindViewById<View>(Resource.Id.letter4Layout);
            _letter4Button = FindViewById<Button>(Resource.Id.letter4Button);
            _letter5Layout = FindViewById<View>(Resource.Id.letter5Layout);
            _letter5Button = FindViewById<Button>(Resource.Id.letter5Button);
            _letter6Layout = FindViewById<View>(Resource.Id.letter6Layout);
            _letter6Button = FindViewById<Button>(Resource.Id.letter6Button);
            _letter7Layout = FindViewById<View>(Resource.Id.letter7Layout);
            _letter7Button = FindViewById<Button>(Resource.Id.letter7Button);
            _letter8Layout = FindViewById<View>(Resource.Id.letter8Layout);
            _letter8Button = FindViewById<Button>(Resource.Id.letter8Button);
            _letter9Layout = FindViewById<View>(Resource.Id.letter9Layout);
            _letter9Button = FindViewById<Button>(Resource.Id.letter9Button);
            _letter10Layout = FindViewById<View>(Resource.Id.letter10Layout);
            _letter10Button = FindViewById<Button>(Resource.Id.letter10Button);
            _letter11Layout = FindViewById<View>(Resource.Id.letter11Layout);
            _letter11Button = FindViewById<Button>(Resource.Id.letter11Button);
            _letter12Layout = FindViewById<View>(Resource.Id.letter12Layout);
            _letter12Button = FindViewById<Button>(Resource.Id.letter12Button);
            _letter13Layout = FindViewById<View>(Resource.Id.letter13Layout);
            _letter13Button = FindViewById<Button>(Resource.Id.letter13Button);
            _letter14Layout = FindViewById<View>(Resource.Id.letter14Layout);
            _letter14Button = FindViewById<Button>(Resource.Id.letter14Button);
            _letter15Layout = FindViewById<View>(Resource.Id.letter15Layout);
            _letter15Button = FindViewById<Button>(Resource.Id.letter15Button);
            _letter16Layout = FindViewById<View>(Resource.Id.letter16Layout);
            _letter16Button = FindViewById<Button>(Resource.Id.letter16Button);
            _letter17Layout = FindViewById<View>(Resource.Id.letter17Layout);
            _letter17Button = FindViewById<Button>(Resource.Id.letter17Button);
            _letter18Layout = FindViewById<View>(Resource.Id.letter18Layout);
            _letter18Button = FindViewById<Button>(Resource.Id.letter18Button);
            _letter19Layout = FindViewById<View>(Resource.Id.letter19Layout);
            _letter19Button = FindViewById<Button>(Resource.Id.letter19Button);
            _letter20Layout = FindViewById<View>(Resource.Id.letter20Layout);
            _letter20Button = FindViewById<Button>(Resource.Id.letter20Button);
            _letter21Layout = FindViewById<View>(Resource.Id.letter21Layout);
            _letter21Button = FindViewById<Button>(Resource.Id.letter21Button);
            _letter22Layout = FindViewById<View>(Resource.Id.letter22Layout);
            _letter22Button = FindViewById<Button>(Resource.Id.letter22Button);
            _letter23Layout = FindViewById<View>(Resource.Id.letter23Layout);
            _letter23Button = FindViewById<Button>(Resource.Id.letter23Button);
            _letter24Layout = FindViewById<View>(Resource.Id.letter24Layout);
            _letter24Button = FindViewById<Button>(Resource.Id.letter24Button);
            _letter25Layout = FindViewById<View>(Resource.Id.letter25Layout);
            _letter25Button = FindViewById<Button>(Resource.Id.letter25Button);
            _letter26Layout = FindViewById<View>(Resource.Id.letter26Layout);
            _letter26Button = FindViewById<Button>(Resource.Id.letter26Button);
            _letter27Layout = FindViewById<View>(Resource.Id.letter27Layout);
            _letter27Button = FindViewById<Button>(Resource.Id.letter27Button);
            _letter28Layout = FindViewById<View>(Resource.Id.letter28Layout);
            _letter28Button = FindViewById<Button>(Resource.Id.letter28Button);
            _letter29Layout = FindViewById<View>(Resource.Id.letter29Layout);
            _letter29Button = FindViewById<Button>(Resource.Id.letter29Button);
            _letter30Layout = FindViewById<View>(Resource.Id.letter30Layout);
            _letter30Button = FindViewById<Button>(Resource.Id.letter30Button);
            _letter31Layout = FindViewById<View>(Resource.Id.letter31Layout);
            _letter31Button = FindViewById<Button>(Resource.Id.letter31Button);
            _letter32Layout = FindViewById<View>(Resource.Id.letter32Layout);
            _letter32Button = FindViewById<Button>(Resource.Id.letter32Button);
            _letter33Layout = FindViewById<View>(Resource.Id.letter33Layout);
            _letter33Button = FindViewById<Button>(Resource.Id.letter33Button);
            _letter34Layout = FindViewById<View>(Resource.Id.letter34Layout);
            _letter34Button = FindViewById<Button>(Resource.Id.letter34Button);
            _letter35Layout = FindViewById<View>(Resource.Id.letter35Layout);
            _letter35Button = FindViewById<Button>(Resource.Id.letter35Button);
            _letter36Layout = FindViewById<View>(Resource.Id.letter36Layout);
            _letter36Button = FindViewById<Button>(Resource.Id.letter36Button);
            _letter37Layout = FindViewById<View>(Resource.Id.letter37Layout);
            _letter37Button = FindViewById<Button>(Resource.Id.letter37Button);
            _letter38Layout = FindViewById<View>(Resource.Id.letter38Layout);
            _letter38Button = FindViewById<Button>(Resource.Id.letter38Button);
            _letter39Layout = FindViewById<View>(Resource.Id.letter39Layout);
            _letter39Button = FindViewById<Button>(Resource.Id.letter39Button);
            _letter40Layout = FindViewById<View>(Resource.Id.letter40Layout);
            _letter40Button = FindViewById<Button>(Resource.Id.letter40Button);

            _word1layout = FindViewById<View>(Resource.Id.word1layout);
            _word1button = FindViewById<Button>(Resource.Id.word1button);
            _word2layout = FindViewById<View>(Resource.Id.word2layout);
            _word2button = FindViewById<Button>(Resource.Id.word2button);
            _word3layout = FindViewById<View>(Resource.Id.word3layout);
            _word3button = FindViewById<Button>(Resource.Id.word3button);
            _word4layout = FindViewById<View>(Resource.Id.word4layout);
            _word4button = FindViewById<Button>(Resource.Id.word4button);
            _word5layout = FindViewById<View>(Resource.Id.word5layout);
            _word5button = FindViewById<Button>(Resource.Id.word5button);
            _word6layout = FindViewById<View>(Resource.Id.word6layout);
            _word6button = FindViewById<Button>(Resource.Id.word6button);
            _word7layout = FindViewById<View>(Resource.Id.word7layout);
            _word7button = FindViewById<Button>(Resource.Id.word7button);
            _word8layout = FindViewById<View>(Resource.Id.word8layout);
            _word8button = FindViewById<Button>(Resource.Id.word8button);
            _word9layout = FindViewById<View>(Resource.Id.word9layout);
            _word9button = FindViewById<Button>(Resource.Id.word9button);
            _word10layout = FindViewById<View>(Resource.Id.word10layout);
            _word10button = FindViewById<Button>(Resource.Id.word10button);

            _letter1Layout.Click += OnLetterClicked;
            _letter2Layout.Click += OnLetterClicked;
            _letter3Layout.Click += OnLetterClicked;
            _letter4Layout.Click += OnLetterClicked;
            _letter5Layout.Click += OnLetterClicked;
            _letter6Layout.Click += OnLetterClicked;
            _letter7Layout.Click += OnLetterClicked;
            _letter8Layout.Click += OnLetterClicked;
            _letter9Layout.Click += OnLetterClicked;
            _letter10Layout.Click += OnLetterClicked;
            _letter11Layout.Click += OnLetterClicked;
            _letter12Layout.Click += OnLetterClicked;
            _letter13Layout.Click += OnLetterClicked;
            _letter14Layout.Click += OnLetterClicked;
            _letter15Layout.Click += OnLetterClicked;
            _letter16Layout.Click += OnLetterClicked;
            _letter17Layout.Click += OnLetterClicked;
            _letter18Layout.Click += OnLetterClicked;
            _letter19Layout.Click += OnLetterClicked;
            _letter20Layout.Click += OnLetterClicked;
            _letter21Layout.Click += OnLetterClicked;
            _letter22Layout.Click += OnLetterClicked;
            _letter23Layout.Click += OnLetterClicked;
            _letter24Layout.Click += OnLetterClicked;
            _letter25Layout.Click += OnLetterClicked;
            _letter26Layout.Click += OnLetterClicked;
            _letter27Layout.Click += OnLetterClicked;
            _letter28Layout.Click += OnLetterClicked;
            _letter29Layout.Click += OnLetterClicked;
            _letter30Layout.Click += OnLetterClicked;
            _letter31Layout.Click += OnLetterClicked;
            _letter32Layout.Click += OnLetterClicked;
            _letter33Layout.Click += OnLetterClicked;
            _letter34Layout.Click += OnLetterClicked;
            _letter35Layout.Click += OnLetterClicked;
            _letter36Layout.Click += OnLetterClicked;
            _letter37Layout.Click += OnLetterClicked;
            _letter38Layout.Click += OnLetterClicked;
            _letter39Layout.Click += OnLetterClicked;
            _letter40Layout.Click += OnLetterClicked;

            _hintButton = FindViewById<AppCompatImageButton>(Resource.Id.hintButton);
            _backButton = FindViewById<AppCompatImageButton>(Resource.Id.backButton);

            _hintButton.Enabled = !_hintWasClicked;
            _hintButton.Clickable = !_hintWasClicked;
            _hintButton.Visibility = _hintWasClicked ? ViewStates.Invisible : ViewStates.Visible;

            _hintButton.Click += OnHintButtonClicked;
            _backButton.Click += (s, a) =>
            {
                Finish();
            };
        }

        private void OnHintButtonClicked(object sender, EventArgs e)
        {
            if (_hintWasClicked)
                return;

            for (var i = 0; i < 40; i++)
            {
                if (_currentWord.ToUpper().Contains(_alphabetArray[i].ToUpper()) && _alphabetIntArray[i] == 1)
                {
                    _alphabetIntArray[i] = 0;
                    RefreshAlphabet();
                    _hintWasClicked = true;
                    _hintButton.Enabled = false;
                    _hintButton.Clickable = false;
                    _hintButton.Visibility = ViewStates.Invisible;
                    return;
                }
            }
        }

        private void ShowFinishAlert(bool wasGuessed)
        {
            if (_myDialog != null && _myDialog.IsShowing)
                return;

            _inactive = true;

            if (!wasGuessed)
            {
                var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                    .SetTitle(_currentWord.ToUpper())
                    .SetMessage(string.Format(Resources.GetString(Resource.String.UnguessedWordText), _currentWord.ToUpper()))
                    .SetNegativeButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                    .SetPositiveButton(Resources.GetString(Resource.String.ContinueButton), ContinueGame)
                    .SetCancelable(false)
                    .Create();

                _myDialog = dialog;

                _myDialog.Show();
            }
            else
            {
                var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                    .SetTitle(_currentWord.ToUpper())
                    .SetMessage($"{Resources.GetString(Resource.String.CorrectToastText)} {_currentWord.ToUpper()}!")
                    .SetNegativeButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                    .SetPositiveButton(Resources.GetString(Resource.String.ContinueButton), ContinueGame)
                    .SetCancelable(false)
                    .Create();

                _myDialog = dialog;

                _myDialog.Show();

                if (_levelIndex == 2)
                    _preferencesHelper.PutGuessHardWord(this);
                else
                    _preferencesHelper.PutGuessWord(this);
            }
        }

        private async void ContinueGame(object sender, DialogClickEventArgs e)
        {
            _inactive = false;

            if (!_progressDialog.IsShowing)
                _progressDialog.Show();

            if (_wordsDatabase == null)
                _wordsDatabase = new WordsDatabase(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));

            var wordInfo = await Task.Run(() => _wordsDatabase.GetItem(_categoryIndex, _levelIndex, _exceptIds.ToArray()));
            _currentWord = wordInfo.Word;
            _currentWordId = wordInfo.Id;
            
            if (string.IsNullOrWhiteSpace(_currentWord) && _currentWordId == -1)
            {
                if (_progressDialog.IsShowing)
                    _progressDialog.Dismiss();
                ShowAllWordsGuessedAlert();
                return;
            }
                        
            _alphabetIntArray = new int[40];
            _alphabetArray = new string[40];
            _lifes = 7;
            RunOnUiThread(() =>
            {
                Glide.With(this).Load(Resource.Drawable.bridge_default).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
            });

            for (int i = 0; i < 40; i++)
            {
                if (i < GameHelper.GetAlphabet(_currentLocale).Length)
                {
                    _alphabetIntArray[i] = 1;
                    _alphabetArray[i] = GameHelper.GetAlphabet(_currentLocale)[i];
                }
                else
                {
                    _alphabetIntArray[i] = 2;
                    _alphabetArray[i] = string.Empty;
                }
            }
            
            _needFinishActivity = false;
            _hintWasClicked = false;
            _hintButton.Clickable = true;
            _hintButton.Enabled = true;
            _hintButton.Visibility = ViewStates.Visible;

            _word1button.Text = _word2button.Text = _word3button.Text = _word4button.Text =
                _word5button.Text = _word6button.Text = _word7button.Text = _word8button.Text =
                _word9button.Text = _word10button.Text = string.Empty;

            InitGameAndStart();

            if (_progressDialog.IsShowing)
                _progressDialog.Dismiss();
        }

        private void ShowAllWordsGuessedAlert()
        {
            var dialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AlertDialogTheme)
                    .SetTitle(_currentWord.ToUpper())
                    .SetMessage(Resources.GetString(Resource.String.AllWordsGuessed))
                    .SetNegativeButton(Resources.GetString(Resource.String.CloseButton), CloseDialog)
                    .SetCancelable(false)
                    .Create();

            _myDialog = dialog;

            _myDialog.Show();
        }

        private void CloseDialog(object sender, DialogClickEventArgs e)
        {
            _inactive = false;
            ((Android.Support.V7.App.AlertDialog)sender).Dismiss();
            Finish();
        }

        private async void OnLetterClicked(object sender, EventArgs e)
        {
            if (_inactive)
                return;

            if (_needFinishActivity)
            {
                Intent myIntent = new Intent(this, typeof(MainActivity));
                myIntent.PutExtra("currentWord", _currentWord.ToUpper());
                myIntent.PutExtra("wordWasGuessed", false);
                SetResult(Result.Ok, myIntent);
                Finish();
                return;
            }

            if (_lifes <= 0)
            {
                RunOnUiThread(() =>
                {
                    Glide.Get(this).ClearMemory();
                });

                await Task.Factory.StartNew(() => Glide.Get(this).ClearDiskCache());

                RunOnUiThread(() =>
                {
                    Glide.With(this).Load(Resource.Drawable.bridge_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                });

                await Task.Delay(2000);

                RunOnUiThread(() =>
                {
                    Glide.Get(this).ClearMemory();
                });

                await Task.Factory.StartNew(() => Glide.Get(this).ClearDiskCache());

                RunOnUiThread(() =>
                {
                    Glide.With(this).Load(Resource.Drawable.bridge_7_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                });
                ShowFinishAlert(false);
                return;
            }

            var layout = (View)sender;
            switch (layout.Id)
            {
                case Resource.Id.letter1Layout:
                    _alphabetIntArray[0] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter1Button);
                    break;
                case Resource.Id.letter2Layout:
                    _alphabetIntArray[1] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter2Button);
                    break;
                case Resource.Id.letter3Layout:
                    _alphabetIntArray[2] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter3Button);
                    break;
                case Resource.Id.letter4Layout:
                    _alphabetIntArray[3] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter4Button);
                    break;
                case Resource.Id.letter5Layout:
                    _alphabetIntArray[4] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter5Button);
                    break;
                case Resource.Id.letter6Layout:
                    _alphabetIntArray[5] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter6Button);
                    break;
                case Resource.Id.letter7Layout:
                    _alphabetIntArray[6] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter7Button);
                    break;
                case Resource.Id.letter8Layout:
                    _alphabetIntArray[7] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter8Button);
                    break;
                case Resource.Id.letter9Layout:
                    _alphabetIntArray[8] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter9Button);
                    break;
                case Resource.Id.letter10Layout:
                    _alphabetIntArray[9] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter10Button);
                    break;
                case Resource.Id.letter11Layout:
                    _alphabetIntArray[10] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter11Button);
                    break;
                case Resource.Id.letter12Layout:
                    _alphabetIntArray[11] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter12Button);
                    break;
                case Resource.Id.letter13Layout:
                    _alphabetIntArray[12] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter13Button);
                    break;
                case Resource.Id.letter14Layout:
                    _alphabetIntArray[13] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter14Button);
                    break;
                case Resource.Id.letter15Layout:
                    _alphabetIntArray[14] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter15Button);
                    break;
                case Resource.Id.letter16Layout:
                    _alphabetIntArray[15] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter16Button);
                    break;
                case Resource.Id.letter17Layout:
                    _alphabetIntArray[16] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter17Button);
                    break;
                case Resource.Id.letter18Layout:
                    _alphabetIntArray[17] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter18Button);
                    break;
                case Resource.Id.letter19Layout:
                    _alphabetIntArray[18] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter19Button);
                    break;
                case Resource.Id.letter20Layout:
                    _alphabetIntArray[19] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter20Button);
                    break;
                case Resource.Id.letter21Layout:
                    _alphabetIntArray[20] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter21Button);
                    break;
                case Resource.Id.letter22Layout:
                    _alphabetIntArray[21] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter22Button);
                    break;
                case Resource.Id.letter23Layout:
                    _alphabetIntArray[22] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter23Button);
                    break;
                case Resource.Id.letter24Layout:
                    _alphabetIntArray[23] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter24Button);
                    break;
                case Resource.Id.letter25Layout:
                    _alphabetIntArray[24] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter25Button);
                    break;
                case Resource.Id.letter26Layout:
                    _alphabetIntArray[25] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter26Button);
                    break;
                case Resource.Id.letter27Layout:
                    _alphabetIntArray[26] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter27Button);
                    break;
                case Resource.Id.letter28Layout:
                    _alphabetIntArray[27] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter28Button);
                    break;
                case Resource.Id.letter29Layout:
                    _alphabetIntArray[28] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter29Button);
                    break;
                case Resource.Id.letter30Layout:
                    _alphabetIntArray[29] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter30Button);
                    break;
                case Resource.Id.letter31Layout:
                    _alphabetIntArray[30] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter31Button);
                    break;
                case Resource.Id.letter32Layout:
                    _alphabetIntArray[31] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter32Button);
                    break;
                case Resource.Id.letter33Layout:
                    _alphabetIntArray[32] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter33Button);
                    break;
                case Resource.Id.letter34Layout:
                    _alphabetIntArray[33] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter34Button);
                    break;
                case Resource.Id.letter35Layout:
                    _alphabetIntArray[34] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter35Button);
                    break;
                case Resource.Id.letter36Layout:
                    _alphabetIntArray[35] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter36Button);
                    break;
                case Resource.Id.letter37Layout:
                    _alphabetIntArray[36] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter37Button);
                    break;
                case Resource.Id.letter38Layout:
                    _alphabetIntArray[37] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter38Button);
                    break;
                case Resource.Id.letter39Layout:
                    _alphabetIntArray[38] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter39Button);
                    break;
                case Resource.Id.letter40Layout:
                    _alphabetIntArray[39] = 0;
                    RefreshAlphabet();
                    ProccessLifes(_letter40Button);
                    break;
            }                
        }

        private async void ProccessLifes(Button buttonPressed)
        {
            if (!(_currentWord.ToUpper().Contains(buttonPressed.Text.ToUpper())))
            {
                _lifes--;

                switch(_lifes)
                {
                    case 6:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_1).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_1_1).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    case 5:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_2).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_2_2).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    case 4:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_3).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_3_3).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    case 3:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_4).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_4_4).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    case 2:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_5).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_5_5).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    case 1:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_6).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_6_6).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    case 0:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_7_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                    default:
                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });

                        await Task.Delay(1000);

                        RunOnUiThread(() =>
                        {
                            Glide.With(this).Load(Resource.Drawable.bridge_7_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                        });
                        break;
                }

                if (_lifes <= 0)
                {
                    if (_friendMode)
                        _needFinishActivity = true;

                    RunOnUiThread(() =>
                    {
                        Glide.With(this).Load(Resource.Drawable.bridge_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                    });

                    await Task.Delay(1000);

                    RunOnUiThread(() =>
                    {
                        Glide.With(this).Load(Resource.Drawable.bridge_7_7).DiskCacheStrategy(DiskCacheStrategy.None)
                        .SkipMemoryCache(true).Override(500, 500).Into(_bridgeImage);
                    });
                }
            }
        }

        private void Anim_AnimationEnd(object sender, Animation.AnimationEndEventArgs e)
        {
            if (_friendMode)
            {
                Intent myIntent = new Intent(this, typeof(MainActivity));
                myIntent.PutExtra("currentWord", _currentWord.ToUpper());
                myIntent.PutExtra("wordWasGuessed", false);
                SetResult(Result.Ok, myIntent);
                Finish();
            }
            else
            {
                ShowFinishAlert(false);
            }
        }
    }
}