using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content.PM;
using Android.OS;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;

namespace dihei_empty_node.Droid
{
    [Activity(Label = "dihei_empty_node.old.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        // Define a authenticated user.
        private MobileServiceUser user;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

			// Initialize Xamarin Forms
			Forms.Init(this, bundle);

			// Initialize the authenticator before loading the app.
			App.Init(this);

			// Load the main application
			LoadApplication(new App());
        }

		public async Task<bool> Authenticate()
		{
			var success = false;
			var message = string.Empty;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
				user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
					MobileServiceAuthenticationProvider.Google, "url_scheme_of_your_app");
				if (user != null)
				{
					message = string.Format("you are now signed-in as {0}.",
						user.UserId);
					success = true;
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			// Display the success or failure message.
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetMessage(message);
			builder.SetTitle("Sign-in result");
			builder.Create().Show();

			return success;
		}
    }
}
