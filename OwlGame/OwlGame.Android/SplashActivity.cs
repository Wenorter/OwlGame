using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OwlGame.Droid
{
    //Student Num: c3260058
    //Date: 01/10/18
    //Description: This code is for app's splash screen and it's has been written to be active before MainActivity.cs within the app.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true, //This makes it to launch first, then go to the title screen.
        Label = "A New Home",
        NoHistory = true)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume() //resumes 
        {
            base.OnResume();
            Task strartupwork = new Task(() => { SimulateStartup(); });
            strartupwork.Start();
        }

        async void SimulateStartup()
        {
            //here you can change the appering time of the splash screen, here it is set to 2000 which means 2 sec.
            await Task.Delay(2000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity))); //this should redirect app to MainActivity class.
            Finish();
        }
    }
}