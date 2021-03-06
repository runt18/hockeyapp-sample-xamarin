﻿using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using HockeyApp;
using System.Threading.Tasks;
using System.IO;

namespace HockeyAppSampleiOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        const string HOCKEYAPP_APPID = "YOUR-HOCKEYAPP-APP-ID";

        UINavigationController navController;
        HomeViewController homeViewController;
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {			
            InitHockeyApp();
			
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            homeViewController = new HomeViewController();
            navController = new UINavigationController(homeViewController);
            window.RootViewController = navController;

            window.MakeKeyAndVisible();
            return true;
        }

        private void InitHockeyApp()
        {
            var manager = BITHockeyManager.SharedHockeyManager;       
            manager.ConfigureWithIdentifier(HOCKEYAPP_APPID,HOCKEYAPP_APPID,new CustomCrashDelegate());
            manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
            manager.StartManager ();
            manager.Authenticator.AuthenticateInstallation ();
        }
    }

    public class CustomCrashDelegate:BITCrashManagerDelegate
    {
        //Called at the next restart after a crash, the content of the file will be visible
        //in the HockeyApp dashboard under the "Description" tab
        public override string ApplicationLogForCrashManager(BITCrashManager crashManager)
        {
            {
                return File.ReadAllText("temp.log");
            }
        }
    }
}
