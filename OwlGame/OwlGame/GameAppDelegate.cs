using System;
using CocosSharp;
using OwlGame.Scenes;
using CocosDenshion;

namespace OwlGame
{
    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 05/10/18
    //Description: This code is for making a Game App authorise and use resources locate din the assets folders for iOS and Android.
    //Tutorial Used: Coin Time Demo in Microsoft Website, https://developer.apple.com/library/archive/documentation/iPhone/Conceptual/iPhoneOSProgrammingGuide/StrategiesforHandlingAppStateTransitions/StrategiesforHandlingAppStateTransitions.html

    public class GameAppDelegate : CCApplicationDelegate
    {
        static CCDirector director;
        static CCWindow mainWindow;

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            GameAppDelegate.mainWindow = mainWindow;
            director = new CCDirector();

            application.PreferMultiSampling = false;
            application.ContentRootDirectory = "Content";
            application.ContentSearchPaths.Add("animations");
            application.ContentSearchPaths.Add("fonts");
            application.ContentSearchPaths.Add("images");
            application.ContentSearchPaths.Add("levels");
            application.ContentSearchPaths.Add("sounds");

#if __IOS__

			application.ContentSearchPaths.Add ("sounds/iOS/");


#else // android
            application.ContentSearchPaths.Add("sounds/Android/");


#endif


            mainWindow.AddSceneDirector(director);


            //=============================================================
            //                   !!!TESTING AREA!!!
            //=============================================================


            /*var scene = new GameScene(mainWindow); */ /*< --------SKIPS TO GAMESCENE*/
            /*var scene = new LevelSelectScene(mainWindow);*/ /*< -------SKIPS TO LEVELSELECTSCENE*/
            var scene = new TitleScene(mainWindow); /*<-------- SLIPS TO TITLESCENE
            /*var scene = new DeathScene(mainWindow);*/ /*<-------- SLIPS TO DEATHSCENE*/
             director.RunWithScene(scene);

            //=============================================================
            //                   !!!TESTING AREA!!!
            //=============================================================
        }

        public override void ApplicationDidEnterBackground(CCApplication application)
        {
            application.Paused = true;
        }

        public override void ApplicationWillEnterForeground(CCApplication application)
        {
            application.Paused = false;
        }

        // For this game we're just going to handle moving between scenes here
        // A larger game might have a "flow" object responsible for moving between
        // scenes.
        public static void GoToGameScene()
        {
            var scene = new GameScene(mainWindow);
            director.ReplaceScene(scene);
        }

        public static void GoToLevelSelectScene()
        {
            CCSimpleAudioEngine.SharedEngine.StopBackgroundMusic(true);
            CCSimpleAudioEngine.SharedEngine.PlayEffect("Button");/* Donwloaded from http://www.pachd.com/button-sounds-2.html*/
            var scene = new LevelSelectScene(mainWindow);
            director.ReplaceScene(scene);
        }

        public static void GoToHowToScene()
        {
            CCSimpleAudioEngine.SharedEngine.StopBackgroundMusic(true);
            CCSimpleAudioEngine.SharedEngine.PlayEffect("Button");
            var scene = new HowToPlayScene(mainWindow);
            director.ReplaceScene(scene);
        }

        public static void GoToTitleScene()
        {
            CCSimpleAudioEngine.SharedEngine.StopBackgroundMusic(true);
            CCSimpleAudioEngine.SharedEngine.PlayEffect("Button");
            var scene = new TitleScene(mainWindow);
            director.ReplaceScene(scene);
        }

        public static void GoToDeathScene()
        {
            CCSimpleAudioEngine.SharedEngine.StopBackgroundMusic(true);
            var scene = new DeathScene(mainWindow);
            director.ReplaceScene(scene);
        }

    }
}
