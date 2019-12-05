using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace TextReplace.Droid
{
    [Activity(Label = "TextReplace", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private MainPageModel model = new MainPageModel();
        const int REQUEST_TEXT_GET = 1;

        private void SelectFile()
        {
            var intent = new Intent(Intent.ActionGetContent);
            intent.SetType("text/plain");
            intent.AddCategory(Intent.CategoryOpenable);
            StartActivityForResult(intent, REQUEST_TEXT_GET);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            var page = App.Current.MainPage as MainPage;
            page.Model = model;
            model.SelectFile = SelectFile;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                switch (requestCode)
                {
                    case REQUEST_TEXT_GET:
                        model.Path = PathUtil.GetActualPathFromFile(this, data.Data);
                        break;
                }
            }
        }
    }
}