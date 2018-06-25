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

        private bool _friendWordMode;
        private string _currentWord;
        private int[] _alphabetIntArray;
        private string[] _alphabetArray;
        private ProgressDialog _progressDialog;
        private Locales _currentLocale;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            _friendWordMode = Intent.GetBooleanExtra(nameof(_friendWordMode), false);

            if (_friendWordMode)
                _currentWord = Intent.GetStringExtra(nameof(_currentWord)) ?? string.Empty;

            var currentLanguage = Locale.Default.Language;
            _currentLocale = currentLanguage == "es" ? Locales.Spain : currentLanguage == "ru" ? Locales.Russian : Locales.English;

            _alphabetIntArray = new int[GameHelper.GetAlphabet(_currentLocale).Length];
            _alphabetArray = GameHelper.GetAlphabet(_currentLocale);
            for (int i = 0; i < GameHelper.GetAlphabet(_currentLocale).Length; i++)
            {
                _alphabetIntArray[i] = 1;
            }

            if (savedInstanceState != null)
            {
                _friendWordMode = savedInstanceState.GetBoolean(nameof(_friendWordMode));
                _currentWord = savedInstanceState.GetString(nameof(_currentWord));
                _alphabetIntArray = savedInstanceState.GetIntArray(nameof(_alphabetIntArray));
            }

            _progressDialog = new ProgressDialog(this, Resource.Style.ProgressDialogTheme) { Indeterminate = true };
            _progressDialog.SetCancelable(false);
            _progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            _progressDialog.SetMessage("Загрузка...");

            SetContentView(Resource.Layout.game);

            InitViews();

            InitGameAndStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        private void InitGameAndStart()
        {

        }

        private void OpenLetter(string letter)
        {
            for (var i = 0; i < _currentWord.Length; i++)
            {
                if (_currentWord[i].ToString().Equals(letter))
                {
                    switch(i)
                    {
                        case 0:
                            _word1button.Text = letter;
                            break;
                        case 1:
                            _word2button.Text = letter;
                            break;
                        case 2:
                            _word3button.Text = letter;
                            break;
                        case 3:
                            _word4button.Text = letter;
                            break;
                        case 4:
                            _word5button.Text = letter;
                            break;
                        case 5:
                            _word6button.Text = letter;
                            break;
                        case 6:
                            _word7button.Text = letter;
                            break;
                        case 7:
                            _word8button.Text = letter;
                            break;
                        case 8:
                            _word9button.Text = letter;
                            break;
                        case 9:
                            _word10button.Text = letter;
                            break;
                    }
                }
            }
        }

        private void RefreshAlphabet()
        {
            _letter1Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter1Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter2Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter2Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter3Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter3Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter4Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter4Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter5Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter5Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter6Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter6Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter7Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter7Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter8Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter8Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter9Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter9Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter10Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter10Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter11Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter11Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter12Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter12Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter13Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter13Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter14Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter14Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter15Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter15Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter16Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter16Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter17Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter17Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter18Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter18Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter19Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter19Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter20Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter20Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter21Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter21Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter22Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter22Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter23Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter23Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter24Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter24Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter25Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter25Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter26Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter26Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter27Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter27Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter28Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter28Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter29Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter29Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter30Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter30Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter31Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter31Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter32Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter32Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter33Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter33Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter34Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter34Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter35Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter35Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter36Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter36Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter37Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter37Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter38Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter38Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter39Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter39Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);
            _letter40Layout.Enabled = _alphabetIntArray[0] == 1;
            _letter40Button.SetBackgroundResource(_alphabetIntArray[0] == 1 ? Resource.Drawable.button_background : Resource.Drawable.button_disabled);

            for (var i = 0; i < _alphabetIntArray.Length; i++)
            {
                if (_alphabetIntArray[i] == 0)
                    OpenLetter(_alphabetArray[i]);
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

            outState.PutBoolean(nameof(_friendWordMode), _friendWordMode);
            outState.PutString(nameof(_currentWord), _currentWord);
            outState.PutIntArray(nameof(_alphabetIntArray), _alphabetIntArray);
        }

        public static Intent CreateStartIntent(Context context, bool friendWordMode = false, string friendWord = null)
        {
            var intent = new Intent(context, typeof(GameActivity));

            intent.PutExtra(nameof(_friendWordMode), friendWordMode);

            if (friendWordMode)
                intent.PutExtra(nameof(_currentWord), friendWord);
            
            return intent;
        }

        private void InitViews()
        {
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
        }

        private void OnLetterClicked(object sender, EventArgs e)
        {
            var layout = (View)sender;
            switch (layout.Id)
            {
                case Resource.Id.letter1Layout:
                    _alphabetIntArray[0] = 0;
                    break;
                case Resource.Id.letter2Layout:
                    _alphabetIntArray[1] = 0;
                    break;
                case Resource.Id.letter3Layout:
                    _alphabetIntArray[2] = 0;
                    break;
                case Resource.Id.letter4Layout:
                    _alphabetIntArray[3] = 0;
                    break;
                case Resource.Id.letter5Layout:
                    _alphabetIntArray[4] = 0;
                    break;
                case Resource.Id.letter6Layout:
                    _alphabetIntArray[5] = 0;
                    break;
                case Resource.Id.letter7Layout:
                    _alphabetIntArray[6] = 0;
                    break;
                case Resource.Id.letter8Layout:
                    _alphabetIntArray[7] = 0;
                    break;
                case Resource.Id.letter9Layout:
                    _alphabetIntArray[8] = 0;
                    break;
                case Resource.Id.letter10Layout:
                    _alphabetIntArray[9] = 0;
                    break;
                case Resource.Id.letter11Layout:
                    _alphabetIntArray[10] = 0;
                    break;
                case Resource.Id.letter12Layout:
                    _alphabetIntArray[11] = 0;
                    break;
                case Resource.Id.letter13Layout:
                    _alphabetIntArray[12] = 0;
                    break;
                case Resource.Id.letter14Layout:
                    _alphabetIntArray[13] = 0;
                    break;
                case Resource.Id.letter15Layout:
                    _alphabetIntArray[14] = 0;
                    break;
                case Resource.Id.letter16Layout:
                    _alphabetIntArray[15] = 0;
                    break;
                case Resource.Id.letter17Layout:
                    _alphabetIntArray[16] = 0;
                    break;
                case Resource.Id.letter18Layout:
                    _alphabetIntArray[17] = 0;
                    break;
                case Resource.Id.letter19Layout:
                    _alphabetIntArray[18] = 0;
                    break;
                case Resource.Id.letter20Layout:
                    _alphabetIntArray[19] = 0;
                    break;
                case Resource.Id.letter21Layout:
                    _alphabetIntArray[20] = 0;
                    break;
                case Resource.Id.letter22Layout:
                    _alphabetIntArray[21] = 0;
                    break;
                case Resource.Id.letter23Layout:
                    _alphabetIntArray[22] = 0;
                    break;
                case Resource.Id.letter24Layout:
                    _alphabetIntArray[23] = 0;
                    break;
                case Resource.Id.letter25Layout:
                    _alphabetIntArray[24] = 0;
                    break;
                case Resource.Id.letter26Layout:
                    _alphabetIntArray[25] = 0;
                    break;
                case Resource.Id.letter27Layout:
                    _alphabetIntArray[26] = 0;
                    break;
                case Resource.Id.letter28Layout:
                    _alphabetIntArray[27] = 0;
                    break;
                case Resource.Id.letter29Layout:
                    _alphabetIntArray[28] = 0;
                    break;
                case Resource.Id.letter30Layout:
                    _alphabetIntArray[29] = 0;
                    break;
                case Resource.Id.letter31Layout:
                    _alphabetIntArray[30] = 0;
                    break;
                case Resource.Id.letter32Layout:
                    _alphabetIntArray[31] = 0;
                    break;
                case Resource.Id.letter33Layout:
                    _alphabetIntArray[32] = 0;
                    break;
                case Resource.Id.letter34Layout:
                    _alphabetIntArray[33] = 0;
                    break;
                case Resource.Id.letter35Layout:
                    _alphabetIntArray[34] = 0;
                    break;
                case Resource.Id.letter36Layout:
                    _alphabetIntArray[35] = 0;
                    break;
                case Resource.Id.letter37Layout:
                    _alphabetIntArray[36] = 0;
                    break;
                case Resource.Id.letter38Layout:
                    _alphabetIntArray[37] = 0;
                    break;
                case Resource.Id.letter39Layout:
                    _alphabetIntArray[38] = 0;
                    break;
                case Resource.Id.letter40Layout:
                    _alphabetIntArray[39] = 0;
                    break;
            }

            RefreshAlphabet();
        }
    }
}