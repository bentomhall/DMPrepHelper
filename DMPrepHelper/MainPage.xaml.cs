using System.Collections.Generic;
using System.Linq;
using DMPrepHelper.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DMPrepHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<ConfigLabel> items = new List<ConfigLabel>
        {
            new ConfigLabel { Icon="Home", Label="Home", Tag="home"},
            new ConfigLabel { Icon="People", Label="NPC Generator", Tag="npcs"},
            new ConfigLabel { Icon="Street", Label="Settlement Generator", Tag="towns"},
            new ConfigLabel { Icon="Map", Label="Dungeon Generator", Tag="dungeon"},
            new ConfigLabel { Icon="Globe", Label="Config Editor", Tag="configs"}
        };

        public MainPage()
        {
            this.InitializeComponent();
            storage = new StorageHelper();

        }

        public List<ConfigLabel> Items { get => items; }

        private StorageHelper storage;

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {

            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            if (args.IsSettingsInvoked)
            {
                //ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);

            }
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                NavView_Navigate(item);
            }
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "home":
                    ContentFrame.Navigate(typeof(Views.WelcomePage));
                    break;

                case "npcs":
                    ContentFrame.Navigate(typeof(Views.NpcPage));
                    break;

                case "towns":
                    ContentFrame.Navigate(typeof(Views.SettlementPage));
                    break;

                case "dungeon":
                    ContentFrame.Navigate(typeof(Views.DungeonPage));
                    break;

                case "configs":
                    ContentFrame.Navigate(typeof(Views.ConfigEditorPage));
                    break;
            }
        }
     
    }
}
