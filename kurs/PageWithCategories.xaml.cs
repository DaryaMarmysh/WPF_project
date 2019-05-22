using kurs.Context;
using kurs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для PageWithCategories.xaml
    /// </summary>
    public partial class PageWithCategories : Page
    {
        
       // private int _curI;
        private List<Category> _categoryList;
        private List<Button> _buttons;

        public Test Test { get; set; }

        public PageWithCategories()
        {
          
            Test = new Test();
            DataContext = Test;
            using (var db = new DatabaseContext())
            {
                _categoryList = db.Categories.Select(p => p).ToList();
            }

            InitializeComponent();
            ShowCategories(null, new RoutedEventArgs());
        }

        private void ShowCategories(object sender, RoutedEventArgs args)
        {
            _buttons = new List<Button>();
            BringButtonsToFront();

        }

        private void BringButtonsToFront()
        {
            using (var db = new DatabaseContext())
            {
                var answers = db.Categories.ToList();  
                CreateButtonsList(answers);
                Test.Buttons = _buttons;
            }
        }

        private void CreateButtonsList(ICollection<Category> categories)
        {
            if (categories == null) return;
            foreach (var category in categories)
            {
                var button = new Button()
                {
                    Content = category.Title,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 14, 0, 0),
                    MinWidth = 200,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Tag=category.Id

                };
                button.Click += SelectCategory ;
                _buttons.Add(button);
            }
        }
        private void SelectCategory(object sender, RoutedEventArgs args)
        {
            Button b = (Button)sender;
            PageWithTitles.MyCurrentTag = int.Parse(b.Tag.ToString());
            this.NavigationService.Navigate(new Uri("PageWithTitles.xaml", UriKind.RelativeOrAbsolute));
            // this.NavigationService.Navigate(new Uri("Testing.xaml", UriKind.RelativeOrAbsolute));
        }
        //private void SaveAnswer()
        //{
        //    using (var db = new DatabaseContext())
        //    {
        //        var point = db.Points.FirstOrDefault(p => p.Title.Equals(TitleLabel.Content.ToString()));
        //        db.Users.FirstOrDefault(u => u.Id == CurrentUser.User.Id)?.PassedPoints.Add(point);
        //        db.SaveChanges();
        //    }
        //}

        //private void IncreaseCounter(string content)
        //{
        //    var title = TitleLabel.Content == null ? string.Empty : TitleLabel.Content.ToString();
        //    using (var db = new DatabaseContext())
        //    {
        //        try
        //        {
        //            db.Points.Include(p => p.Answers).FirstOrDefault(p => p.Title.Equals(title)).Answers
        //                .FirstOrDefault(a => a.Content.Equals(content)).Counter++;
        //            db.SaveChanges();
        //        }
        //        catch (NullReferenceException exception)
        //        {
        //        }
        //    }
        //}
    }
}
