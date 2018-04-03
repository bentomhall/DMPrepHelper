using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DMPrepHelper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();
        }

        public string FeedbackText { get; set; }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var storage = ((App)Application.Current).Storage;
            var filesLoaded = await storage.LoadDataPackage();
            if (filesLoaded == 0)
            {
                FeedbackText = "No suitable files found";
            } else
            {
                FeedbackText = $"Loaded {filesLoaded} files";
            }
        }
    }
}
