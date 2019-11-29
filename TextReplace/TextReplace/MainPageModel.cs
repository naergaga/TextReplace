using System;
using System.Collections.Generic;
using System.Text;

namespace TextReplace
{
    public delegate void SelectFile();


    public class MainPageModel
    {
        public SelectFile SelectFile { get; set; }
    }
}
