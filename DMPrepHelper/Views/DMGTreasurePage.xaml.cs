using DMPrepHelper.ViewModels;
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
    public sealed partial class DMGTreasurePage : Page
    {
        public DMGTreasurePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var storage = ((App)Application.Current).Storage;
            if (ViewModel == null)
            {
                ViewModel = new DMGTreasureGenerator(storage);
            }
            base.OnNavigatedTo(e);
        }

        public DMGTreasureGenerator ViewModel { get; private set; }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var removed = e.RemovedItems;
            var selected = e.AddedItems;
            if (removed.Count != 0)
            {
                foreach (var r in removed)
                {
                    ViewModel.SelectedViewModels.Remove(r as DMGTreasureViewModel);
                }
            }
            if (selected.Count != 0)
            {
                foreach (var a in selected)
                {
                    ViewModel.SelectedViewModels.Add(a as DMGTreasureViewModel);
                }
            }
        }
    }
}
