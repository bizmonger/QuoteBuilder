using Mediation.LocalStorage;
using Xamarin.Forms;
using QuoteBuilder.Droid;

[assembly: Dependency(typeof(NativeURL_Android))]
namespace QuoteBuilder.Droid
{
    public class NativeURL_Android : INativeURL
    {
        public string Acquire() => "file:///android_asset/";
    }
}