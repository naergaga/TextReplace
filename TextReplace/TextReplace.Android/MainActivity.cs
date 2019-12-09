using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android;

namespace TextReplace.Droid
{
    [Activity(Label = "TextReplace", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private MainPageModel model = new MainPageModel();
        const int REQUEST_TEXT_GET = 1;
        const int REQUEST_EXTERNAL_STORAGE = 2;

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

        protected override void OnStart()
        {
            CheckPermission();
            base.OnStart();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void CheckPermission()
        {
            //检查权限（NEED_PERMISSION）是否被授权 PackageManager.PERMISSION_GRANTED表示同意授权
            if (CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != Permission.Granted || CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                //用户已经拒绝过一次，再次弹出权限申请对话框需要给用户一个解释
                if (ShouldShowRequestPermissionRationale(Manifest.Permission.WriteExternalStorage) || ShouldShowRequestPermissionRationale(Manifest.Permission.ReadExternalStorage))
                {
                    Toast.MakeText(this, "请开通相关权限，否则无法正常使用本应用！", ToastLength.Short).Show();
                }
                //申请权限
                this.RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage },
                REQUEST_EXTERNAL_STORAGE
                );

            }
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