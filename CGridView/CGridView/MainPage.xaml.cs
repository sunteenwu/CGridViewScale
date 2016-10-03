using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CGridView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Category> categories = new ObservableCollection<Category> { };
        public MainPage()
        {
            this.InitializeComponent();
            categories = new ObservableCollection<Category>
            {
                new Category {Name="name1",details="color1" ,backgroundcolor="#D90015"},
                new Category {Name="name2",details="color2" ,backgroundcolor="#DC1C17"},
                new Category {Name="name3",details="cplor3",backgroundcolor="#DE3A17" },
                new Category {Name="name3",details="color4",backgroundcolor="#E25819" }
            };
        }
     
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
 

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SolidColorBrush[] ColorArray = new SolidColorBrush[4]
                   {
                new SolidColorBrush( Color.FromArgb(200, 217, 0, 21)),
                new SolidColorBrush( Color.FromArgb(200, 220, 28, 23)),
                new SolidColorBrush( Color.FromArgb(200, 222, 58, 23)),
                new SolidColorBrush( Color.FromArgb(200, 226, 88, 25)) };
            if (CategoryGridView.Items.Count > 4)
            {
                await new Windows.UI.Popups.MessageDialog("There're no more enougth colors").ShowAsync();
            }
            IEnumerable<GridViewItem> items = FindVisualChildren<GridViewItem>(CategoryGridView);

            for (int i = 0; i < 4; i++)
            {
                items.ElementAt<GridViewItem>(i).Background = ColorArray[i];
            }

        }
    }

    public class Category
    {
        public string Name { get; set; }
        public string details { get; set; }
        public string backgroundcolor { get; set; }
    }
}
