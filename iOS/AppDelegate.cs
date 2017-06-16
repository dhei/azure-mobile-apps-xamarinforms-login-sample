using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace dihei_empty_node.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate, IAuthenticate
    {
        // Define a authenticated user.
        private MobileServiceUser user;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // Initialize Azure Mobile Apps
            CurrentPlatform.Init();

            // Initialize Xamarin Forms
            Forms.Init();

            // Initialize the authenticator before loading the app.
            App.Init(this);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return TodoItemManager.DefaultManager.CurrentClient.ResumeWithURL(url);
        }

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                if (user == null)
                {
                    user = await TodoItemManager.DefaultManager.CurrentClient
                        .LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
                            MobileServiceAuthenticationProvider.Google, "zumoe2etestapp");
                    if (user != null)
                    {
                        message = string.Format("You are now signed-in as {0}.", user.UserId);
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            UIAlertView avAlert = new UIAlertView("Sign-in result", message, null, "OK", null);
            avAlert.Show();

            return success;
        }
    }
}
