using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using CocosSharp;
using OwlGame.Input;

namespace OwlGame.Droid
{
    //Student Num: c3260058
    //Date: 05/10/18
    //Description: This code is for app's main activity and is responisble for all the activity after the splash screen.
    //Tutorial Used: Coin Time Demo in Microsoft Website
    [Activity(
        Label = "A New Home",
        AlwaysRetainTaskState = true,
        Icon = "@drawable/icon",
        Theme = "@android:style/Theme.NoTitleBar",
        ScreenOrientation = ScreenOrientation.Landscape | ScreenOrientation.ReverseLandscape,
        LaunchMode = LaunchMode.SingleInstance,
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)
    ]
    public class MainActivity : AndroidGameActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);
            
            ContentLoading.XmlDeserializer.Self.Activity = this;
            var application = new CCApplication();
            application.ApplicationDelegate = new GameAppDelegate(); //https://forums.xamarin.com/discussion/55345/cocossharp-v1-7-0-0-pre1-embeddable-game-view-and-xamarin-forms-support
            SetContentView(application.AndroidContentView);
            application.StartGame();
            application.AndroidContentView.RequestFocus();
            application.AndroidContentView.KeyPress += HandleKeyPress;
        }



        void HandleKeyPress(object sender, View.KeyEventArgs args)
        {
            Console.WriteLine(args.Event + " " + args.KeyCode + " ");

            // Check if the down times equals the event time. If not, then it's a long press:
            if (args.Event.Action == KeyEventActions.Down && args.Event.EventTime == args.Event.DownTime)
            {
                AmazonFireGameController.HandlePush(args.KeyCode);
            }
            else if (args.Event.Action == KeyEventActions.Up)
            {
                // OwlGame doesn't currently use it currently.
                //AmazonFireGameController.HandleRelease (args.KeyCode);
            }
        }

        public override bool OnGenericMotionEvent(MotionEvent e)
        {
            if (e.Source != InputSourceType.Touchscreen)
            {
                AmazonFireGameController.SetDPad(e.GetAxisValue(Axis.HatX));
                AmazonFireGameController.SetLeftAnalogStick(e.GetAxisValue(Axis.X));

                return true;
            }
            else
            {
                return false;
            }
        }
    }


}

