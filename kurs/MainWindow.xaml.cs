using System;
using System.Collections.Generic;
using System.Windows;
using kurs.Models;
using System.Linq;
namespace kurs
{
    public partial class MainWindow
    {
        public readonly List<string> Styles = new List<string> { "Green", "Amber","BlueGrey","Brown","DeepOrange","DeepPurple",
               "Grey","LightGreen","Lime","Orange","Pink","Teal","Yellow" };

        private int i = 0;
        private int j = 0;
        public MainWindow()
        {
            InitializeComponent();
            if (CurrentUser.User.Role.Equals("Admin"))
            {
                Builder.Visibility = Visibility.Visible;
            }
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("pack://application:,,,/Images.xaml");
            MainGrid.Background = (System.Windows.Media.Brush)resourceDictionary[CurrentUser.User.Theme.ToString()];
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(3);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() {
            Source = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{Styles[CurrentUser.User.Theme]}.xaml")});
        }


        private void ExitFromApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("pack://application:,,,/Images.xaml");
            Testing temp = new Testing();
            string style = "";
            if (i >= Styles.Count - 1)
            { i = 0; }
            style = Styles[i];
            MainGrid.Background = (System.Windows.Media.Brush)resourceDictionary[i.ToString()];
            Uri uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{style}.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(3);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });
            CurrentUser.User.Theme = i;
            using (var db = new DatabaseContext())
            {

                db.Users.FirstOrDefault(u => u.Id == CurrentUser.User.Id).Theme = i;
                db.SaveChanges();
            }
            i++;
           
          
        }

        private void ShowTests(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("PageWithCategories.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BuildTest(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Constructor.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
