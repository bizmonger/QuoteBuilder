using System.Diagnostics.CodeAnalysis;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace QuoteBuilder.Droid
{
    [Activity(Label = "QuoteBuilder", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        [SuppressMessage("Potential Code Quality Issues", "RECS0133:Parameter name differs in base declaration", Justification = "<Pending>")]
        protected override void OnCreate(Bundle bundle)
        {
            PromiseEmailClient();

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new QuoteBuilder.App());
        }

        public void PromiseEmailClient() =>
            Subscribe(Messages.REQUEST_EMAIL_CLIENT, obj =>
                Publish(Messages.REQUEST_EMAIL_CLIENT_RESPONSE, new EmailClient()));
    }
}