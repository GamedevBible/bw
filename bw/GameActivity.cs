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
        private bool _friendWordMode;
        private string _currentWord;
        private int[] _alphabetIntArray;
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
        }

        protected override void OnResume()
        {
            base.OnResume();

            InitGameAndStart();
        }

        private void InitGameAndStart()
        {

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
    }
}