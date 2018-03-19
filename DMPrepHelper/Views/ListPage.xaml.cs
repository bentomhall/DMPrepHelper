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
    public sealed partial class ListPage : Page
    {
        private StorageHelper storage;
        private ViewModels.SimpleListDataModel model;

        public ListPage()
        {
            this.InitializeComponent();
            storage = ((App)Application.Current).Storage;
        }

        private void SetupViewModel(string type)
        {
            switch (type)
            {
                case "personality":
                    ViewModel = new ViewModels.SimpleListDataModel(storage, DataFile.Personality);
                    break;
                case "profession":
                    ViewModel = new ViewModels.SimpleListDataModel(storage, DataFile.Profession);
                    break;
                default:
                    break;
            }
            return;
        }

        public ViewModels.SimpleListDataModel ViewModel { get => model; set => model = value; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetupViewModel(e.Parameter as string);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.SaveConfigCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }
    }
}
