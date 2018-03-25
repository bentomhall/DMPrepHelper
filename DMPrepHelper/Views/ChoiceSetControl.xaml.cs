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
using DMPrepHelper.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DMPrepHelper.Views
{
    public sealed partial class ChoiceSetControl : UserControl, INotifyPropertyChanged
    {

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(ObservableCollection<DictItem>), typeof(ChoiceSetControl), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(DictItem), typeof(ChoiceSetControl), new PropertyMetadata(null));
        public static readonly DependencyProperty AddItemProperty = DependencyProperty.Register("AddItem", typeof(ICommand), typeof(ChoiceSetControl), new PropertyMetadata(false, OnChangeCallback));
        public static readonly DependencyProperty RemoveItemProperty = DependencyProperty.Register("RemoveItem", typeof(ICommand), typeof(ChoiceSetControl), new PropertyMetadata(false, OnChangeCallback));

        public ICommand AddItem { get => (ICommand)GetValue(AddItemProperty); set => SetValue(AddItemProperty, value); }
        public ICommand RemoveItem { get => (ICommand)GetValue(RemoveItemProperty); set => SetValue(RemoveItemProperty, value); }

        public ObservableCollection<DictItem> ItemSource { get => (ObservableCollection<DictItem>)GetValue(ItemSourceProperty); set => SetValue(ItemSourceProperty, value); }
        public DictItem SelectedItem { get => (DictItem)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(ChoiceSetControl), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        public ChoiceSetControl()
        {
            this.InitializeComponent();
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var buttonName = (sender as Button).Name;
            if (buttonName == "AddListItemButton")
            {
                AddItem.Execute("");
            }
            else if (buttonName == "DeleteListItemButton")
            {
                RemoveItem.Execute("");
            }
        }

        private void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private static void OnChangeCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as ChoiceSetControl;
            s?.OnPropertyChanged(nameof(ItemSource));
        }

    }
}
