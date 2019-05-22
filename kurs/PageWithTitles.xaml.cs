using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using kurs.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kurs.Context;


namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для PageWithTitles.xaml
    /// </summary>
    public partial class PageWithTitles : Page
    {
        private List<TestTitle> _titleList;
        private List<Button> _buttons;

        public Test Test { get; set; }
        public static int MyCurrentTag;
        public PageWithTitles()
        {
            
            Test = new Test();
            DataContext = Test;
            using (var db = new DatabaseContext())
            {
                _titleList = db.TestTitles.Where(p=>p.CategoryId==MyCurrentTag).ToList();
            }

            InitializeComponent();
            ShowTitles(null, new RoutedEventArgs());
        }

        // private int _curI;
        

        

        private void ShowTitles(object sender, RoutedEventArgs args)
        {
            _buttons = new List<Button>();
            BringButtonsToFront();

        }

        private void BringButtonsToFront()
        {
            using (var db = new DatabaseContext())
            {
                var titles = db.TestTitles.Where(p => p.CategoryId == MyCurrentTag).ToList();
                CreateButtonsList(titles);
                Test.Buttons = _buttons;
            }
        }

        private void CreateButtonsList(ICollection<TestTitle> titles)
        {
            if (titles == null) return;
            foreach (var title in titles)
            {
                var button = new Button()
                {
                    Content = title.Title,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 14, 0, 0),
                    MinWidth = 200,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Tag= title.Id


                };
                button.Click += SelectCategory;
                _buttons.Add(button);
            }
        }
        private void SelectCategory(object sender, RoutedEventArgs args)
        {
            Button b = (Button)sender;
            Testing.MyCurrentTest = int.Parse(b.Tag.ToString());
            this.NavigationService.Navigate(new Uri("Testing.xaml", UriKind.RelativeOrAbsolute));
            // this.NavigationService.Navigate(new Uri("Testing.xaml", UriKind.RelativeOrAbsolute));
            
        }
    }
}
