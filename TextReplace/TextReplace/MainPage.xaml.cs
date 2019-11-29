using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TextReplace
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPageModel Model { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }


        public void SelectFileClick(object sender, EventArgs e)
        {
            Model.SelectFile();
        }


        public void ReplaceClick(object sender, EventArgs e)
        {
            
        }
    }
}
