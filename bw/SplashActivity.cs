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
using Android.Content.PM;
using System.Threading.Tasks;
using Android.Support.V7.App;

namespace bw
{
    [Activity(Label = "@string/ApplicationName", Theme = "@style/ActivitySplash", MainLauncher = true, 
        ScreenOrientation = ScreenOrientation.Portrait,
        Icon = "@mipmap/ic_launcher")]
    public class SplashActivity : AppCompatActivity
    {
        private bool _needStartApp = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                _needStartApp = savedInstanceState.GetBoolean(nameof(_needStartApp));
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutBoolean(nameof(_needStartApp), _needStartApp);
        }

        protected override void OnResume()
        {
            base.OnResume();

            /*MobileCenter.Start("40d1e1c0-0450-4ef1-bda4-8d5f1365f069",
                   typeof(Analytics), typeof(Crashes));*/ // PLAY MARKET

            if (!_needStartApp)
            {
                return;
            }

            _needStartApp = false;

            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(200);
            StartActivity(MainActivity.CreateStartIntent(this));
        }
    }
}